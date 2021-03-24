using Avertory.Models.BindingModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avertory.Validation
{
	public class InventoryBindingModelValidator : AbstractValidator<InventoryBindingModel>
	{
		public InventoryBindingModelValidator()
		{
			RuleFor(x => x.DateOfInventory).NotEmpty();
			RuleFor(x => x.Identifier).NotEmpty();
			RuleFor(x => x.Location).NotEmpty();
			RuleFor(x => x.Tags).NotEmpty();
		}
	}
}
