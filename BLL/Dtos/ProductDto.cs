using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string CompanyName { get; set; }
		public ulong CompanyPrefix { get; set; }
		public string ItemName { get; set; }
		public ulong ItemReference { get; set; }
	}
}
