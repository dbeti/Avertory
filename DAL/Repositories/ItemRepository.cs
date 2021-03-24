using DAL.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class ItemRepository : IItemRepository
	{
		private readonly AvertoryDbContext _avertoryDbContext;

		public ItemRepository(AvertoryDbContext avertoryDbContext)
		{
			_avertoryDbContext = avertoryDbContext;
		}

		/// <inheritdoc/>
		public async Task<bool> CheckSerialNumbersAsync(IEnumerable<ulong> serialNumbers)
		{
			var doesExist = await _avertoryDbContext.Items.AnyAsync(x => serialNumbers.Contains(x.SerialNumber));
			return doesExist;
		}

		/// <inheritdoc/>
		public async Task<int> GetItemsCountByCompanyAsync(ulong companyPrefix)
		{
			var count = await _avertoryDbContext.Items
				.Where(x => x.Product.CompanyPrefix == companyPrefix)
				.CountAsync();

			return count;
		}

		/// <inheritdoc/>
		public async Task<int> GetItemsCountByProductAndInventoryAsync(ulong itemReference, string inventoryIdentifier)
		{
			var count = await _avertoryDbContext.Items
				.Where(x => x.Inventory.Identifier == inventoryIdentifier
					&& x.Product.ItemReference == itemReference)
				.CountAsync();

			return count;
		}

		/// <inheritdoc/>
		public async Task<Dictionary<DateTime, int>> GetItemsCountByProductByDayAsync(ulong itemReference)
		{
			var countPerDay = await _avertoryDbContext.Items
				.Where(x => x.Product.ItemReference == itemReference)
				.GroupBy(x => x.Inventory.DateOfInventory)
				.Select(x => new { x.Key, Count = x.Count()})
				.ToDictionaryAsync(x => x.Key, x => x.Count);

			return countPerDay;
		}

		/// <inheritdoc/>
		public async Task InsertItemsAsync(IEnumerable<Item> items)
		{
			await _avertoryDbContext.Items.AddRangeAsync(items);
			await _avertoryDbContext.SaveChangesAsync();
		}
	}
}
