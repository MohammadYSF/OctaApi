using FluentValidation;
using OctaShared.DTOs.Request;

namespace Command.Core.Application.Features.InvoiceFeatures.AddnvoicePayment
{
    public class AddInvoicePaymentValidator : AbstractValidator<AddInvoicePaymentRequest>
    {
        public AddInvoicePaymentValidator()
        {
            RuleFor(request => request.TrackCodeAndAmountList)
    .Must(TrackCodeAndAmountListIsValid)
    .WithMessage("All amounts in the TrackCodeAndAmountList must be non-negative.");

        }
        private bool TrackCodeAndAmountListIsValid(List<Tuple<string, long>> trackCodeAndAmountList)
        {
            foreach (var tuple in trackCodeAndAmountList)
            {
                if (tuple.Item2 < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
