using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.EntityModels
{
	public class Product
	{
		public int Id { get; set; }

		public ulong CompanyPrefix { get; set; }

		public string CompanyName { get; set; }

		public ulong ItemReference { get; set; }

		public string ItemName { get; set; }
	}
}
