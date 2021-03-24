using Avertory.Models.BindingModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avertory.Validation
{
	public class ProductBindingModelValidator : AbstractValidator<ProductBindingModel>
	{
		public ProductBindingModelValidator()
		{
			RuleFor(x => x.CompanyName).NotEmpty();
			RuleFor(x => x.CompanyPrefix).NotEmpty();
			RuleFor(x => x.ItemReference).NotEmpty();
			RuleFor(x => x.ItemName).NotEmpty();
		}
	}
}
