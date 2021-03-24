using BLL.Dtos;
using DAL.Models.EntityModels;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class InventoryService : IInventoryService
	{
		private readonly ISGTIN96Service _sGTIN96Service;
		private readonly IItemRepository _itemRepository;
		private readonly IProductRepository _productRepository;
		private readonly IInventoryRepository _inventoryRepository;

		public InventoryService(ISGTIN96Service sGTIN96Service,
			IItemRepository itemRepository,
			IProductRepository productRepository,
			IInventoryRepository inventoryRepository)
		{
			_sGTIN96Service = sGTIN96Service;
			_itemRepository = itemRepository;
			_productRepository = productRepository;
			_inventoryRepository = inventoryRepository;
		}

		/// <inheritdoc/>
		public bool DoesCompanyExist(ulong companyPrefix)
		{
			var doesExist = _productRepository.DoesCompanyExist(companyPrefix);
			return doesExist;
		}

		/// <inheritdoc/>
		public bool DoesInventoryExist(string inventoryIdentifier)
		{
			var doesExist =  _inventoryRepository.DoesInventoryExist(inventoryIdentifier);
			return doesExist;
		}

		/// <inheritdoc/>
		public bool DoesProductExist(ulong itemReference)
		{
			var doesExist = _productRepository.DoesProductExist(itemReference);
			return doesExist;
		}

		/// <inheritdoc/>
		public async Task<int> GetItemsCountByCompanyAsync(ulong companyPrefix)
		{
			var count = await _itemRepository.GetItemsCountByCompanyAsync(companyPrefix);
			return count;
		}

		/// <inheritdoc/>
		public async Task<int> GetItemsCountByProductAndInventoryAsync(ulong itemReference, string inventoryIdentifier)
		{
			var count = await _itemRepository.GetItemsCountByProductAndInventoryAsync(itemReference, inventoryIdentifier);
			return count;
		}

		/// <inheritdoc/>
		public async Task<Dictionary<DateTime, int>> GetItemsCountByProductByDayAsync(ulong itemReference)
		{
			var countPerDay = await _itemRepository.GetItemsCountByProductByDayAsync(itemReference);
			return countPerDay;
		}

		/// <inheritdoc/>
		public async Task InsertInventoryAsync(InventoryDto inventoryDto)
		{
			var decodedTags = inventoryDto.Tags
				.Select(x => _sGTIN96Service.Decode(x))
				.ToArray();

			var groupedTags = decodedTags
				.GroupBy(x => x.ItemReference)
				.ToDictionary(x => x.Key, x => x.ToArray());

			var itemReferences = groupedTags.Keys;

			var requestedCompanyPrefixes = decodedTags
				.Select(x => x.CompanyPrefix)
				.Distinct()
				.ToArray();

			var serialNumbers = decodedTags.Select(x => x.SerialNumber).ToArray();
			var productsByItemReference = await _productRepository.GetProductsByItemReferenceAsync(groupedTags.Keys);
			var serialNumbersExist = await _itemRepository.CheckSerialNumbersAsync(serialNumbers);
			var inventoryExists = _inventoryRepository.DoesInventoryExist(inventoryDto.Identifier);

			var doItemReferencesExist = itemReferences.All(x => productsByItemReference.Keys.Contains(x));

			var companyPrefixes = productsByItemReference.Select(x => x.Value.CompanyPrefix).ToArray();
			var doCompaniesExist = requestedCompanyPrefixes.All(x => companyPrefixes.Contains(x));

			if (!doItemReferencesExist
				|| !doCompaniesExist)
			{
				throw new ArgumentException("Provided products/companies don't exist in the system!");
			}
			else if (serialNumbersExist)
			{
				throw new ArgumentException("Some provided serial numbers exist in the system!");
			}
			else if (inventoryExists)
			{
				throw new ArgumentException("Provided inventory already exists in the system!");
			}


			var items = new List<Item>();
			var inventory = await _inventoryRepository.InsertAsync(new Inventory
			{
				DateOfInventory = inventoryDto.DateOfInventory,
				Identifier = inventoryDto.Identifier,
				Location = inventoryDto.Location
			});

			foreach (var groupedTag in groupedTags)
			{
				var product = productsByItemReference[groupedTag.Key];
				foreach (var tag in groupedTag.Value)
				{
					var item = new Item
					{
						ProductId = product.Id,
						SerialNumber = tag.SerialNumber,
						InventoryId = inventory.Id
					};
					items.Add(item);
				}
			}

			await _itemRepository.InsertItemsAsync(items);

			return;
		}
	}
}
