using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avertory.Models.BindingModels
{
	public class InventoryBindingModel
	{
		public string Location { get; set; }
		public DateTime DateOfInventory { get; set; }
		public string Identifier { get; set; }
		public IEnumerable<string> Tags { get; set; }
	}
}
