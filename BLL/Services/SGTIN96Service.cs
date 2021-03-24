using BLL.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class SGTIN96Service : ISGTIN96Service
	{
		private const int HEADER_STARTING_BIT = 0;
		private const int HEADER_BIT_COUNT = 8;
		private const int FILTER_STARTING_BIT = 8;
		private const int FILTER_BIT_COUNT = 3;
		private const int PARTITION_STARTING_BIT = 11;
		private const int PARTITION_BIT_COUNT = 3;
		private const int PAYLOAD_STARTING_BIT = 14;
		private const int SGTIN96HEX_LENGTH = 24;

		private readonly ILogger _logger;

		public SGTIN96Service(ILogger<SGTIN96Service> logger)
		{
			_logger = logger;
		}

		/// <inheritdoc/>
		public DecodedSGTIN Decode(string hexSGTIN)
		{
			// hexSGTIN validation
			var isValid = Validate(hexSGTIN);

			if (!isValid)
			{
				throw new ArgumentException("Provided SGTIN is not in a valid hexadecimal format!");
			}

			// Parsing
			var sgtinBits = StringToBitsArray(hexSGTIN);

			var header = DecodeUInt64(sgtinBits, HEADER_STARTING_BIT, HEADER_BIT_COUNT);
			var filter = DecodeUInt64(sgtinBits, FILTER_STARTING_BIT, FILTER_BIT_COUNT);
			var partition = DecodeUInt64(sgtinBits, PARTITION_STARTING_BIT, PARTITION_BIT_COUNT);
			var partitionTable = GetPartitionTable((int)partition);

			var companyPrefix = DecodeUInt64(sgtinBits, PAYLOAD_STARTING_BIT, partitionTable.CompanyPrefixBits);

			var itemReferenceStartingBit = PAYLOAD_STARTING_BIT + partitionTable.CompanyPrefixBits;
			var itemReference = DecodeUInt64(sgtinBits, itemReferenceStartingBit, partitionTable.ItemReferenceBits);
			var serialNumber = DecodeUInt64(sgtinBits, itemReferenceStartingBit + partitionTable.ItemReferenceBits, 38);

			var decodedSGTIN = new DecodedSGTIN()
			{
				CompanyPrefix = companyPrefix,
				ItemReference = itemReference,
				SerialNumber = serialNumber,
				Filter = filter
			};

			return decodedSGTIN;
		}

		private bool Validate(string hexSGTIN)
		{
			if (hexSGTIN.Length != SGTIN96HEX_LENGTH)
			{
				_logger.LogError("Provided SGTIN in the hexadecimal format doesn't have valid length!");
				return false;
			}

			for (int i = 0; i < SGTIN96HEX_LENGTH; i += 2)
			{
				if (!int.TryParse(hexSGTIN.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, null, out int parsedHex))
				{
					_logger.LogError("Provided SGTIN is not in a valid hexadecimal format!");
					return false;
				}
			}

			return true;
		}

		private ulong DecodeUInt64(BitArray bits, int startingBit, int bitCount)
		{
			ulong value = 0;
			for (int i = 0; i < bitCount; ++i)
			{
				if (bits[startingBit + bitCount - i - 1])
				{
					value |= (1u << i);
				}
			}
			return value;
		}

		private BitArray StringToBitsArray(string hex)
		{
			var numberOfCharacters = hex.Length;
			var bytes = new byte[numberOfCharacters / 2];

			for (int i = 0; i < numberOfCharacters; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}

			var bits = new BitArray(bytes);

			// Reverse bits in every byte if system is a little endian
			if (BitConverter.IsLittleEndian)
			{
				bool tempBit;	
				for (var i = 0; i < (numberOfCharacters / 2) * 8; ++i)
				{
					if (i % 8 >= 4)
					{
						continue;
					}

					var toReplace = (8 * ((i / 8) + 1) - (i % 8) - 1);
					tempBit = bits.Get(toReplace);
					bits.Set(toReplace, bits.Get(i));
					bits.Set(i, tempBit);
				}
			}

			return bits;
		}

		private (int CompanyPrefixBits, int ItemReferenceBits) GetPartitionTable(int partition)
		{
			if (partition < 0 || partition > 6)
			{
				throw new ArgumentOutOfRangeException("Partition table is defined only for number between 0 and 6!");
			}

			switch (partition)
			{
				case 0:
					return (40, 4);
				case 1:
					return (37, 7);
				case 2:
					return (34, 10);
				case 3:
					return (30, 14);
				case 4:
					return (27, 17);
				case 5:
					return (24, 20);
				default:
					return (20, 24);
			}
		}
	}
}
