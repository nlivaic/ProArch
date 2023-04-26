using System.Collections.Generic;

namespace ProArch.CodingTest.Invoices.Service
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetInvoices(int supplierId);
    }
}