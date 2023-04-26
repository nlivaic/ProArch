using ProArch.CodingTest.Invoices.Service;
using ProArch.CodingTest.Suppliers.Services;
using System.Linq;

namespace ProArch.CodingTest.Summary
{
    public class SpendService
    {
        private readonly SupplierService _supplierService;
        private readonly InvoiceServiceFactory _invoiceServiceFactory;

        public SpendService(
            SupplierService supplierService,
            InvoiceServiceFactory invoiceServiceFactory)
        {
            _supplierService = supplierService;
            _invoiceServiceFactory = invoiceServiceFactory;
        }

        public SpendSummary GetTotalSpend(int supplierId)
        {
            var supplier = _supplierService.GetById(supplierId);
            var invoiceService = _invoiceServiceFactory.Create(supplier);
            var invoices = invoiceService.GetInvoices(supplier.Id);
            var summary = new SpendSummary
            {
                Years = invoices
                    .GroupBy(i => i.InvoiceDate.Year)
                    .Select(g =>
                        new SpendDetail
                        {
                            Year = g.Key,
                            TotalSpend = g.Sum(i => i.Amount)
                        })
                    .ToList(),
                Name = $"spend_summary_{supplierId}"
            };
            return summary;
        }
    }
}
