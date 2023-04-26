using Moq;
using Moq.AutoMock;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Invoices;
using ProArch.CodingTest.Invoices.Repository;
using ProArch.CodingTest.Invoices.Service;
using ProArch.CodingTest.Invoices.Service.External;
using ProArch.CodingTest.Summary;
using ProArch.CodingTest.Suppliers;
using ProArch.CodingTest.Suppliers.Repository;
using ProArch.CodingTest.Suppliers.Services;

namespace ProArch.CodingTest.UnitTests.Builders
{
    public class SpendServiceBuilder
    {
        private readonly AutoMocker _mocker;

        public SpendServiceBuilder(AutoMocker mocker)
        {
            _mocker = mocker;
        }

        public SpendServiceBuilder BuildForSupplier()
        {
            var supplierInvoiceService = _mocker.CreateInstance<SupplierInvoiceService>();
            _mocker.Use(supplierInvoiceService);
            var invoiceServiceFactory = _mocker.CreateInstance<InvoiceServiceFactory>();
            _mocker.Use(invoiceServiceFactory);
            var supplierService = _mocker.CreateInstance<SupplierService>();
            _mocker.Use(supplierService);
            return this;
        }

        public SpendServiceBuilder BuildForExternalSupplier()
        {
            var externalSupplierInvoiceService = _mocker.CreateInstance<ExternalSupplierInvoiceService>();
            _mocker.Use(externalSupplierInvoiceService);
            var invoiceServiceFactory = _mocker.CreateInstance<InvoiceServiceFactory>();
            _mocker.Use(invoiceServiceFactory);
            var supplierService = _mocker.CreateInstance<SupplierService>();
            _mocker.Use(supplierService);
            return this;
        }

        public SpendServiceBuilder WithSupplier(Supplier supplier)
        {
            var supplierRepositoryMock = _mocker
                .GetMock<ISupplierRepository>()
                .Setup(m => m.Get(supplier.Id))
                .Returns(supplier);
            _mocker.Use(supplierRepositoryMock);
            return this;
        }

        internal SpendServiceBuilder WithInvoices(List<Invoice> invoices)
        {
            var invoiceRepositoryMock = _mocker
                .GetMock<IInvoiceRepository>()
                .Setup(m => m.Get())
                .Returns(invoices.AsQueryable());
            _mocker.Use(invoiceRepositoryMock);
            return this;
        }

        internal SpendServiceBuilder WithExternalInvoices(List<Invoice> invoices, int supplierId)
        {
            var externalInvoiceService = _mocker
                .GetMock<IExternalInvoiceService>()
                .Setup(m => m.GetInvoices(supplierId.ToString()))
                .Returns(invoices.ToArray());
            _mocker.Use(externalInvoiceService);
            return this;
        }

        internal SpendService Create() => _mocker.CreateInstance<SpendService>();
    }
}