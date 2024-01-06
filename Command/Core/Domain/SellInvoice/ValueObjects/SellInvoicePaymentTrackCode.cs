using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.ValueObjects
{
    public sealed class SellInvoicePaymentTrackCode : ValueObject<SellInvoicePaymentTrackCode>
    {
        public SellInvoicePaymentTrackCode(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
        protected override bool EqualsCore(SellInvoicePaymentTrackCode other)
        {
            return other.Value == Value;
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
