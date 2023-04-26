using ProArch.CodingTest.External;
using System.Collections.Generic;

namespace ProArch.CodingTest.Invoices.Service.External
{
    public interface IExternalInvoiceService
    {
        IEnumerable<Invoice> GetInvoices(string supplierId);
    }
}
