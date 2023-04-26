using ProArch.CodingTest.Invoices.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Invoices.Service
{
    public class SupplierInvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public SupplierInvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public IEnumerable<Invoice> GetInvoices(int supplierId) =>
            _invoiceRepository
                .Get()
                .Where(i => i.SupplierId == supplierId)
                .ToList();
    }
}
