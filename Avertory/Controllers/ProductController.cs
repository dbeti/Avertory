using Avertory.Models.BindingModels;
using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avertory.Controllers
{
	[Route("[controller]")]
	[Authorize]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private ILogger<ProductController> _logger;
		private IProductService _productService;

		public ProductController(ILogger<ProductController> logger,
			IProductService productService)
		{
			_logger = logger;
			_productService = productService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			_logger.LogInformation($"Getting a product {id} started.");

			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				_logger.LogInformation($"Product {id} not found. Getting a product finished.");
				return NotFound();
			}

			_logger.LogInformation($"Getting a product finished.");
			return Ok(product);
		}

		[HttpPost("bulk-for-testing")]
		public async Task<IActionResult> Post([FromBody] IEnumerable<ProductBindingModel> productBindingModel)
		{
			_logger.LogInformation($"Posting a new product.");

			var productDtos = productBindingModel.Select(x => new ProductDto
			{
				CompanyName = x.CompanyName,
				CompanyPrefix = x.CompanyPrefix,
				ItemName = x.ItemName,
				ItemReference = x.ItemReference
			});

			var insertedProducts = new List<ProductDto>();
			foreach (var productDto in productDtos)
			{
				var insertedDto = await _productService.InsertAsync(productDto);
				insertedProducts.Add(insertedDto);
			}

			_logger.LogInformation($"Posting a new product finished.");

			return Ok(insertedProducts);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ProductBindingModel productBindingModel)
		{
			_logger.LogInformation($"Posting a new product.");

			var productDto = new ProductDto
			{
				CompanyName = productBindingModel.CompanyName,
				CompanyPrefix = productBindingModel.CompanyPrefix,
				ItemName = productBindingModel.ItemName,
				ItemReference = productBindingModel.ItemReference
			};

			productDto = await _productService.InsertAsync(productDto);
			_logger.LogInformation($"Posting a new product finished.");

			return Ok(productDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] ProductBindingModel productBindingModel)
		{
			_logger.LogInformation($"Modification of the product {id} started.");

			var productDto = new ProductDto
			{
				CompanyName = productBindingModel.CompanyName,
				CompanyPrefix = productBindingModel.CompanyPrefix,
				ItemName = productBindingModel.ItemName,
				ItemReference = productBindingModel.ItemReference,
				Id = id
			};

			productDto = await _productService.UpdateAsync(id, productDto);
			return Ok(productDto);
		}

	}
}
