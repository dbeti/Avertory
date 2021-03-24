using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public interface ISGTIN96Service
	{
		/// <summary>
		/// Decodes the SGTIN from the hexadecimal format to the populated <see cref="DecodedSGTIN"/> dto. 
		/// </summary>
		/// <param name="hexSGTIN">SGTIN in a hexadecimal format in a form of a string.</param>
		/// <returns>Populated decoded SGTIN</returns>
		DecodedSGTIN Decode(string hexSGTIN);
	}
}
