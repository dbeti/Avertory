using DAL.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class InventoryRepository : IInventoryRepository
	{
		private readonly AvertoryDbContext _avertoryDbContext;

		public InventoryRepository(AvertoryDbContext avertoryDbContext)
		{
			_avertoryDbContext = avertoryDbContext;
		}

		/// <inheritdoc/>
		public bool DoesInventoryExist(string inventoryIdentifier)
		{
			var doesExist = _avertoryDbContext.Inventories.Any(x => x.Identifier == inventoryIdentifier);
			return doesExist;
		}

		/// <inheritdoc/>
		public async Task<Inventory> InsertAsync(Inventory inventory)
		{
			await _avertoryDbContext.Inventories.AddAsync(inventory);
			await _avertoryDbContext.SaveChangesAsync();

			return inventory;
		}
	}
}
