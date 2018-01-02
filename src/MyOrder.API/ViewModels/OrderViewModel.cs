using MyOrder.API.ViewModels.Validations;
using MyOrder.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyOrder.API.ViewModels
{
    public class OrderViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int ItemsCount { get; set; }        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new UserViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
