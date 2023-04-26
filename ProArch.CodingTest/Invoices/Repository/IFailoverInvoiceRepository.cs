namespace ProArch.CodingTest.Invoices.Repository
{
    public interface IFailoverInvoiceRepository
    {
        FailoverInvoiceCollection GetBySupplier(int supplierId);
    }
}
