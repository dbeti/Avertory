using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
	public class InventoryDto
	{
		public string Location { get; set; }
		public DateTime DateOfInventory { get; set; }
		public string Identifier { get; set; }
		public IEnumerable<string> Tags { get; set; }
	}
}
