using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.EntityModels
{
	public class Item
	{
		public int Id { get; set; }
		public ulong SerialNumber { get; set; }

		public int InventoryId { get; set; }
		public Inventory Inventory { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
