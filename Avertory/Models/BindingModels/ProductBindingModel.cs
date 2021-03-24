using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avertory.Models.BindingModels
{
	public class ProductBindingModel
	{
		public string CompanyName { get; set; }
		public ulong CompanyPrefix { get; set; }
		public string ItemName { get; set; }
		public ulong ItemReference { get; set; }
	}
}
