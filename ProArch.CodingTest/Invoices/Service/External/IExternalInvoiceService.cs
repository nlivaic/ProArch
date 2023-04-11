using ProArch.CodingTest.External;

namespace ProArch.CodingTest.Invoices.Service.External
{
    public interface IExternalInvoiceService
    {
        ExternalInvoice[] GetInvoices(string supplierId);
    }
}
