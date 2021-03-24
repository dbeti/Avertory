using DAL.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public interface IProductRepository
	{
		/// <summary>
		/// Gets product from the system by id.
		/// </summary>
		/// <param name="productId">Product id that will be fetched from the system.</param>
		/// <returns>Populated product.</returns>
		Task<Product> GetProductByIdAsync(int productId);
		/// <summary>
		/// Gets product by company prefix and item reference.
		/// </summary>
		/// <param name="itemReferences">Item references which product data will be fetched.</param>
		/// <returns>Populated product entities.</returns>
		Task<IDictionary<ulong, Product>> GetProductsByItemReferenceAsync(IEnumerable<ulong> itemReferences);
		/// <summary>
		/// Inserts product.
		/// </summary>
		/// <param name="product">Product that will be inserted.</param>
		/// <returns>Inserted populated product.</returns>
		Task<Product> InsertAsync(Product product);
		/// <summary>
		/// Updates product.
		/// </summary>
		/// <param name="product">Product that will be updated.</param>
		/// <returns>Updated populated product.</returns>
		Task<Product> UpdateAsync(Product product);
		/// <summary>
		/// Checks whether product exists in the persistance storage.
		/// </summary>
		/// <param name="itemReference">Item reference of the product that will be checked.</param>
		/// <returns>True if product exists, false otherwise.</returns>
		bool DoesProductExist(ulong itemReference);
		/// <summary>
		/// Checks whether company exists in the persistance storage.
		/// </summary>
		/// <param name="companyPrefix">Prefix of the company that will be checked.</param>
		/// <returns>True if company exists, false otherwise.</returns>
		bool DoesCompanyExist(ulong companyPrefix);
	}
}
