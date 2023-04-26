using ProArch.CodingTest.Invoices;

namespace ProArch.CodingTest.UnitTests.Builders
{
    internal class InvoiceBuilder
    {
        private readonly List<Invoice> _target = new();

        public InvoiceBuilder AddInvoice(int year, int supplierId, decimal amount)
        {
            _target.Add(new()
            {
                SupplierId = supplierId,
                InvoiceDate = new DateTime(year, 1, 1),
                Amount = amount,
            });
            return this;
        }

        public List<Invoice> Create() => _target;
    }
}
