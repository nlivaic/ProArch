using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Invoices.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public IQueryable<Invoice> Get()
        {
            return new List<Invoice>().AsQueryable();
        }
    }
}
