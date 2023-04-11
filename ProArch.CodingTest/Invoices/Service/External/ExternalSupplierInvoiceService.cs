using System.Collections.Generic;

namespace ProArch.CodingTest.Invoices.Service.External
{
    public class ExternalSupplierInvoiceService : IInvoiceService
    {
        private readonly IExternalInvoiceService _externalInvoiceService;
        private readonly IFailoverInvoiceService _failoverInvoiceService;

        public ExternalSupplierInvoiceService(
            IExternalInvoiceService externalInvoiceService,
            IFailoverInvoiceService failoverInvoiceService)
        {
            _externalInvoiceService = externalInvoiceService;
            _failoverInvoiceService = failoverInvoiceService;
        }

        public IEnumerable<Invoice> GetInvoices(int supplierId)
        {
            throw new System.NotImplementedException();
        }
    }
}
