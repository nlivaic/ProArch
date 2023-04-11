using ProArch.CodingTest.External;
using ExternalInvoiceServiceSDK = ProArch.CodingTest.External.ExternalInvoiceService;

namespace ProArch.CodingTest.Invoices.Service.External
{
    /// <summary>
    /// This is an adapter class, meant to hide our external
    /// dependency (ExternalInvoiceServiceSDK) behind an interface we control.
    /// </summary>
    public class ExternalInvoiceService : IExternalInvoiceService
    {
        public ExternalInvoice[] GetInvoices(string supplierId) =>
            ExternalInvoiceServiceSDK.GetInvoices(supplierId);
    }
}
