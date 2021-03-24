using DAL.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly AvertoryDbContext _avertoryDbContext;

		public ProductRepository(AvertoryDbContext avertoryDbContext)
		{
			_avertoryDbContext = avertoryDbContext;
		}

		/// <inheritdoc/>
		public bool DoesCompanyExist(ulong companyPrefix)
		{
			var doesExists = _avertoryDbContext.Products.Any(x => x.CompanyPrefix == companyPrefix);
			return doesExists;
		}

		/// <inheritdoc/>
		public bool DoesProductExist(ulong itemReference)
		{
			var doesExists = _avertoryDbContext.Products.Any(x => x.ItemReference == itemReference);
			return doesExists;
		}

		/// <inheritdoc/>
		public async Task<Product> GetProductByIdAsync(int productId)
		{
			var product = await _avertoryDbContext.Products
				.Where(x => x.Id == productId)
				.FirstOrDefaultAsync();

			return product;
		}

		/// <inheritdoc/>
		public async Task<IDictionary<ulong, Product>> GetProductsByItemReferenceAsync(IEnumerable<ulong> itemReferences)
		{
			var productsByItemReference = await _avertoryDbContext.Products
				.Where(x => itemReferences.Contains(x.ItemReference))
				.ToDictionaryAsync(x => x.ItemReference, x => x);

			return productsByItemReference;
		}

		/// <inheritdoc/>
		public async Task<Product> InsertAsync(Product product)
		{
			await _avertoryDbContext.Products.AddAsync(product);

			// Unit of work?
			await _avertoryDbContext.SaveChangesAsync();
			return product;
		}

		/// <inheritdoc/>
		public async Task<Product> UpdateAsync(Product product)
		{
			_avertoryDbContext.Products.Update(product);
			await _avertoryDbContext.SaveChangesAsync();

			return product;
		}
	}
}
