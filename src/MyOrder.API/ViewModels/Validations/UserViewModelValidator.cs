using FluentValidation;

namespace MyOrder.API.ViewModels.Validations
{
    public class UserViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(user => user.Address).NotEmpty().WithMessage("Address cannot be empty");
            RuleFor(user => user.City).NotEmpty().WithMessage("City cannot be empty");
        }
    }
}
