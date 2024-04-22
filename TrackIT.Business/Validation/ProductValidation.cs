using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Validation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş bırakılamaz");
            RuleFor(p => p.Serial).NotEmpty().WithMessage("Seri boş bırakılamaz");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategori boş bırakılamaz");
        }
    }
}
