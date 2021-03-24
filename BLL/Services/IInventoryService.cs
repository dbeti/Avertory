using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public interface IInventoryService
	{
		/// <summary>
		/// Inserts inventory in the system.
		/// </summary>
		/// <param name="inventoryDto">Inventory data to be inserted in the system.</param>
		Task InsertInventoryAsync(InventoryDto inventoryDto);
		/// <summary>
		/// Checks whether product exists in the system.
		/// </summary>
		/// <param name="itemReference">Item reference of the product that will be checked.</param>
		/// <returns>True if product exists, false otherwise.</returns>
		bool DoesProductExist(ulong itemReference);
		/// <summary>
		/// Checks whether inventory exists in the system.
		/// </summary>
		/// <param name="inventoryIdentifier">Identifier of the inventory that will be checked.</param>
		/// <returns>True if inventory exists, false otherwise.</returns>
		bool DoesInventoryExist(string inventoryIdentifier);
		/// <summary>
		/// Checks whether company exists in the system.
		/// </summary>
		/// <param name="companyPrefix">Prefix of the company that will be checked.</param>
		/// <returns>True if company exists, false otherwise.</returns>
		bool DoesCompanyExist(ulong companyPrefix);
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
		/// Gets count of the inventoried items by company.
		/// </summary>
		/// <param name="companyPrefix">Prefix of the company which items will be counted.</param>
		/// <returns>Count of the inventoried items.</returns>
		Task<int> GetItemsCountByCompanyAsync(ulong companyPrefix);
	}
}
