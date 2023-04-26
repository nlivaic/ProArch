using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Invoices.Service.External
{
    public class ExternalSupplierInvoiceService : IInvoiceService
    {
        private readonly IExternalInvoiceService _externalInvoiceService;
        private readonly FailoverInvoiceService _failoverInvoiceService;

        public ExternalSupplierInvoiceService(
            IExternalInvoiceService externalInvoiceService,
            FailoverInvoiceService failoverInvoiceService)
        {
            _externalInvoiceService = externalInvoiceService;
            _failoverInvoiceService = failoverInvoiceService;
        }

        public IEnumerable<Invoice> GetInvoices(int supplierId)
        {
            // Implement resilience
            return _externalInvoiceService.GetInvoices(supplierId.ToString()).AsEnumerable();
        }
    }
}
