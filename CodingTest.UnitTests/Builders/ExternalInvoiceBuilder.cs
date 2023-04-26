using ProArch.CodingTest.External;
using ProArch.CodingTest.Invoices;

namespace ProArch.CodingTest.UnitTests.Builders
{
    internal class ExternalInvoiceBuilder
    {
        private readonly List<ExternalInvoice> _target = new ();
        private DateTime _failoverTimestamp;

        public ExternalInvoiceBuilder AddExternalInvoice(int year, decimal amount)
        {
            _target.Add(new ()
            {
                TotalAmount = amount,
                Year = year,
            });
            return this;
        }

        public ExternalInvoiceBuilder WithFreshFailoverTimestamp()
        {
            _failoverTimestamp = DateTime.UtcNow;
            return this;
        }

        public ExternalInvoiceBuilder WithStaleFailoverTimestamp()
        {
            _failoverTimestamp = DateTime.UtcNow.AddYears(-1);
            return this;
        }

        public FailoverInvoiceCollection CreateFailoverInvoiceCollection() =>
            new()
            {
                Invoices = _target.ToArray(),
                Timestamp = _failoverTimestamp
            };

        public List<ExternalInvoice> CreateExternalInvoices() =>
            _target;
    }
}
