using ProArch.CodingTest.Invoices.Service.External;
using ProArch.CodingTest.Suppliers;

namespace ProArch.CodingTest.Invoices.Service
{
    public class InvoiceServiceFactory : IInvoiceServiceFactory
    {
        private readonly SupplierInvoiceService _supplierInvoiceService;
        private readonly ExternalSupplierInvoiceService _externalSupplierInvoiceService;

        public InvoiceServiceFactory(
            SupplierInvoiceService supplierInvoiceService,
            ExternalSupplierInvoiceService externalSupplierInvoiceService)
        {
            _supplierInvoiceService = supplierInvoiceService;
            _externalSupplierInvoiceService = externalSupplierInvoiceService;
        }

        public IInvoiceService Create(Supplier supplier) =>
            supplier.IsExternal
                ? _externalSupplierInvoiceService
                : _supplierInvoiceService;
    }
}
