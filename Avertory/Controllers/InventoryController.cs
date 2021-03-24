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
	public class InventoryController : ControllerBase
	{
		private ILogger<InventoryController> _logger;
		private IInventoryService _inventoryService;

		public InventoryController(ILogger<InventoryController> logger,
			IInventoryService inventoryService)
		{
			_logger = logger;
			_inventoryService = inventoryService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] InventoryBindingModel inventoryBindingModel)
		{
			_logger.LogInformation($"Posting a new inventory.");

			var inventoryDto = new InventoryDto
			{
				Location = inventoryBindingModel.Location,
				DateOfInventory = inventoryBindingModel.DateOfInventory,
				Identifier = inventoryBindingModel.Identifier,
				Tags = inventoryBindingModel.Tags
			};

			await _inventoryService.InsertInventoryAsync(inventoryDto);

			_logger.LogInformation($"Posting of the new inventory finished.");
			
			return Ok();
		}

		[Route("/items/product/{itemReference}/inventory/{inventoryIdentifier}/count")]
		[HttpGet]
		public async Task<IActionResult> GetCountByProductAndInventory(ulong itemReference, string inventoryIdentifier)
		{
			_logger.LogInformation($"Count of inventoried items by product and inventory started!");

			var doesProductExist = _inventoryService.DoesProductExist(itemReference);
			var doesInventoryExist = _inventoryService.DoesInventoryExist(inventoryIdentifier);

			if (!doesProductExist || !doesInventoryExist)
			{
				return NotFound();
			}

			var count = await _inventoryService.GetItemsCountByProductAndInventoryAsync(itemReference, inventoryIdentifier);

			return Ok(new { Count = count }  );
		}

		[Route("/items/product/{itemReference}/count")]
		[HttpGet]
		public async Task<IActionResult> GetCountByProductPerDay(ulong itemReference)
		{
			_logger.LogInformation($"Count of inventoried items by product and inventory started!");

			var doesProductExist = _inventoryService.DoesProductExist(itemReference);

			if (!doesProductExist)
			{
				return NotFound();
			}

			var countPerDay = await _inventoryService.GetItemsCountByProductByDayAsync(itemReference);

			return Ok(countPerDay);
		}

		[Route("/items/company/{companyPrefix}/count")]
		[HttpGet]
		public async Task<IActionResult> GetCountByCompany(ulong companyPrefix)
		{
			_logger.LogInformation($"Count of inventoried items by product and inventory started!");

			var doesCompanyExist = _inventoryService.DoesCompanyExist(companyPrefix);

			if (!doesCompanyExist)
			{
				return NotFound();
			}

			var countByCompany = await _inventoryService.GetItemsCountByCompanyAsync(companyPrefix);

			return Ok(new { Count = countByCompany });
		}
	}
}
