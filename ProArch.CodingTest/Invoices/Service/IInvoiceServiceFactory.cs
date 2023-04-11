using ProArch.CodingTest.Suppliers;

namespace ProArch.CodingTest.Invoices.Service
{
    public interface IInvoiceServiceFactory
    {
        IInvoiceService Create(Supplier supplier);
    }
}