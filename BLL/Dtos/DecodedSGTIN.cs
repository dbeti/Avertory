using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
	public class DecodedSGTIN
	{
		public ulong CompanyPrefix { get; set; }
		public ulong ItemReference { get; set; }
		public ulong SerialNumber { get; set; }
		public ulong Filter { get; set; }
	}
}
