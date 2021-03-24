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
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		
		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		/// <inheritdoc/>
		public async Task<ProductDto> GetProductByIdAsync(int id)
		{
			var product = await _productRepository.GetProductByIdAsync(id);
			var productDto = new ProductDto
			{
				CompanyName = product.CompanyName,
				CompanyPrefix = product.CompanyPrefix,
				ItemName = product.ItemName,
				ItemReference = product.ItemReference,
				Id = product.Id
			};

			return productDto;
		}

		/// <inheritdoc/>
		public async Task<ProductDto> InsertAsync(ProductDto productDto)
		{
			// Use automapper
			var product = new Product
			{
				CompanyName = productDto.CompanyName,
				CompanyPrefix = productDto.CompanyPrefix,
				ItemName = productDto.ItemName,
				ItemReference = productDto.ItemReference
			};

			product = await _productRepository.InsertAsync(product);
			productDto.Id = product.Id;

			return productDto;
		}

		/// <inheritdoc/>
		public async Task<ProductDto> UpdateAsync(int productId, ProductDto productDto)
		{
			var product = await _productRepository.GetProductByIdAsync(productId);

			product.CompanyName = productDto.CompanyName;
			product.CompanyPrefix = productDto.CompanyPrefix;
			product.ItemName = productDto.ItemName;
			product.ItemReference = product.ItemReference;

			await _productRepository.UpdateAsync(product);

			return productDto;
		}
	}
}
