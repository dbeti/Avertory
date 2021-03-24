using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public interface IProductService
	{
		/// <summary>
		/// Inserts product in the system.
		/// </summary>
		/// <param name="productDto">Product dto with the data needed for the insertion.</param>
		/// <returns>Inserted product dto.</returns>
		Task<ProductDto> InsertAsync(ProductDto productDto);

		/// <summary>
		/// Updates product in the system.
		/// </summary>
		/// <param name="productId">Id of the product that will be updated.</param>
		/// <param name="productDto">Product datat that will be updated.</param>
		/// <returns>Updated populated product data.</returns>
		Task<ProductDto> UpdateAsync(int productId, ProductDto productDto);

		/// <summary>
		/// Gets product from the system by id.
		/// </summary>
		/// <param name="id">Id of the product to get.</param>
		/// <returns>Populated product dto.</returns>
		Task<ProductDto> GetProductByIdAsync(int id);
	}
}
