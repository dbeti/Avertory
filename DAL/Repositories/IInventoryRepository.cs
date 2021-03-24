using DAL.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public interface IInventoryRepository
	{
		/// <summary>
		/// Inserts inventory to the system.
		/// </summary>
		/// <param name="inventory">Inventory that will be inserted to the system.</param>
		/// <returns>Inserted inventory.</returns>
		Task<Inventory> InsertAsync(Inventory inventory);

		/// <summary>
		/// Checks whether inventory exists in the persistance storage.
		/// </summary>
		/// <param name="inventoryIdentifier">Identifier of the inventory that will be checked.</param>
		/// <returns>True if inventory exists, false otherwise.</returns>
		bool DoesInventoryExist(string inventoryIdentifier);
	}
}
