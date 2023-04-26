using FluentAssertions;
using Moq.AutoMock;
using ProArch.CodingTest.Invoices.Service;
using ProArch.CodingTest.Invoices.Service.External;
using ProArch.CodingTest.UnitTests.Builders;

namespace ProArch.CodingTest.UnitTests
{
    public class InvoiceServiceFactoryTests
    {
        [Fact]
        public void CreatesSupplierInvoiceService_ForSupplier()
        {
            // Arrange
            var mocker = new AutoMocker();
            var supplier = new SupplierBuilder().AsSupplier().Create();
            var supplierInvoiceService = mocker.CreateInstance<SupplierInvoiceService>();
            var externalSupplierInvoiceService = mocker.CreateInstance<ExternalSupplierInvoiceService>();
            var target = new InvoiceServiceFactory(supplierInvoiceService, externalSupplierInvoiceService);

            // Act
            var result = target.Create(supplier);

            // Assert
            result.Should().BeOfType<SupplierInvoiceService>();
        }

        [Fact]
        public void CreatesExternalSupplierInvoiceService_ForExternalSupplier()
        {
            // Arrange
            var mocker = new AutoMocker();
            var supplier = new SupplierBuilder().AsExternalSupplier().Create();
            var supplierInvoiceService = mocker.CreateInstance<SupplierInvoiceService>();
            var externalSupplierInvoiceService = mocker.CreateInstance<ExternalSupplierInvoiceService>();
            var target = new InvoiceServiceFactory(supplierInvoiceService, externalSupplierInvoiceService);

            // Act
            var result = target.Create(supplier);

            // Assert
            result.Should().BeOfType<ExternalSupplierInvoiceService>();
        }
    }
}
