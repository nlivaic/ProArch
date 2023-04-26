using System.Linq;

namespace ProArch.CodingTest.Invoices.Repository
{
    public interface IInvoiceRepository
    {
        IQueryable<Invoice> Get();
    }
}
