using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.EntityModels
{
	public class Inventory
	{
		public int Id { get; set; }
		public string Location { get; set; }
		public DateTime DateOfInventory { get; set; }
		public string Identifier { get; set; }

		public ICollection<Item> Items { get; set; }
	}
}
