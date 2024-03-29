﻿using FluentValidation;
using OctaShared.DTOs.Request;
namespace Command.Core.Application.Features.CustomerFeatures.AddCustomer;
public class AddCustomerValidator : AbstractValidator<AddCustomerRequest>
{
    public AddCustomerValidator()
    {
        RuleFor(a => a.FirstName).NotNull().NotEmpty().MaximumLength(255);
        RuleFor(a => a.LastName).NotNull().NotEmpty().MaximumLength(255);
        RuleFor(a => a.phoneNumber).NotNull().NotEmpty().MaximumLength(15);
        RuleFor(a => a.RegisterDate).NotNull().NotEmpty();
        RuleFor(a => a.VehicleDTOs).Must(a => a.Count > 0);
    }
}
