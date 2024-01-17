using Command.Core.Domain.SellInvoice;

namespace Command.Domain.UnitTest.SellInvoiceAggregateTest
{
    public class SellInvoiceAggregateBuilder
    {
        private Guid _id;
        private DateTime _createDate;
        private int _code;
        private Guid _customer;
        private Guid _vehicle;

        public SellInvoiceAggregate BuildMiscellaneous()
        {
            return SellInvoiceAggregate.CreateMiscellaneous(_id, _createDate, _code);
        }
        public SellInvoiceAggregate Build()
        {
            return SellInvoiceAggregate.Create(_id, _createDate, _code, _customer, _vehicle);
        }
        public SellInvoiceAggregateBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public SellInvoiceAggregateBuilder WithDateTime(DateTime createDate)
        {
            _createDate = createDate;
            return this;
        }
        public SellInvoiceAggregateBuilder WithCode(int code)
        {
            _code = code;
            return this;
        }
        public SellInvoiceAggregateBuilder WithCustomer(Guid customerId)
        {
            _customer = customerId;
            return this;
        }
        public SellInvoiceAggregateBuilder WithVehicle(Guid vehicleId)
        {
            _vehicle = vehicleId;
            return this;

        }
    }
}
