using DAL.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public interface IItemRepository
	{
		/// <summary>
		/// Inserts range of the items to the system.
		/// </summary>
		/// <param name="items">Items that will be inserted to the system.</param>
		Task InsertItemsAsync(IEnumerable<Item> items);
		/// <summary>
		/// Gets count of the inventoried items by company.
		/// </summary>
		/// <param name="companyPrefix">Prefix of the company which items will be counted.</param>
		/// <returns>Count of the inventoried items.</returns>
		Task<int> GetItemsCountByCompanyAsync(ulong companyPrefix);
		/// <summary>
		/// Gets count of the inventoried items by product and inventory.
		/// </summary>
		/// <param name="itemReference">Item reference.</param>
		/// <param name="inventoryIdentifier">Inventory identifier.</param>
		/// <returns>Count of the inventoried items.</returns>
		Task<int> GetItemsCountByProductAndInventoryAsync(ulong itemReference, string inventoryIdentifier);
		/// <summary>
		/// Gets count of the inventoried items by product per day.
		/// </summary>
		/// <param name="itemReference">Item reference of the product that will be checked.</param>
		/// <returns>Count of the inventoried items by day.</returns>
		Task<Dictionary<DateTime, int>> GetItemsCountByProductByDayAsync(ulong itemReference);
		/// <summary>
		/// Checks if provided serial numbers exists in the system. 
		/// </summary>
		/// <param name="serialNumbers">Serial numbers to be checked.</param>
		/// <returns>True if any serial number exists, false otherwise.</returns>
		Task<bool> CheckSerialNumbersAsync(IEnumerable<ulong> serialNumbers);
	}
}
