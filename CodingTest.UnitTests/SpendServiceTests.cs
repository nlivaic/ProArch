using FluentAssertions;
using Moq.AutoMock;
using ProArch.CodingTest.Summary;
using ProArch.CodingTest.Suppliers;
using ProArch.CodingTest.UnitTests.Builders;

namespace ProArch.CodingTest.UnitTests
{

    public class SpendServiceTests
    {
        [Fact]
        public void GetTotalSpend_SingleSupplier_GetsSummarizedSuccessfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var supplier = new SupplierBuilder()
                .AsSupplier()
                .Create();
            var invoices = new InvoiceBuilder()
                .AddInvoice(2023, supplier.Id, 100)
                .Create();
            var target = new SpendServiceBuilder(mocker)
                .BuildForSupplier()
                .WithSupplier(supplier)
                .WithInvoices(invoices)
                .Create();

            // Act
            var result = target.GetTotalSpend(supplier.Id);

            // Assert
            result.Years.Count.Should().Be(1);
            result.Years.First().Year.Should().Be(2023);
            result.Years.First().TotalSpend.Should().Be(100);
        }

        [Fact]
        public void GetTotalSpend_OneSupplier_MultipleInvoicesAcrossMultipleYears_GetSummarizedSuccessfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var supplier = new SupplierBuilder()
                .AsSupplier()
                .Create();
            var invoices = new InvoiceBuilder()
                .AddInvoice(2022, supplier.Id, 100)
                .AddInvoice(2022, supplier.Id, 100)
                .AddInvoice(2023, supplier.Id, 100)
                .AddInvoice(2023, supplier.Id, 100)
                .Create();
            var target = new SpendServiceBuilder(mocker)
                .BuildForSupplier()
                .WithSupplier(supplier)
                .WithInvoices(invoices)
                .Create();

            // Act
            var result = target.GetTotalSpend(supplier.Id);

            // Assert
            result.Years.Count.Should().Be(2);
            result.Years.Single(y => y.Year == 2022).TotalSpend.Should().Be(200);
            result.Years.Single(y => y.Year == 2023).TotalSpend.Should().Be(200);
        }

        [Fact]
        public void GetTotalSpend_TwoSuppliers_GetSummarizedSuccessfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var supplier1 = new SupplierBuilder()
                .AsSupplier()
                .Create();
            var supplier2 = new SupplierBuilder()
                .AsSupplier()
                .Create();
            var invoices = new InvoiceBuilder()
                .AddInvoice(2022, supplier1.Id, 100)
                .AddInvoice(2022, supplier1.Id, 100)
                .AddInvoice(2023, supplier1.Id, 100)
                .AddInvoice(2023, supplier1.Id, 100)
                .Create();
            var target = new SpendServiceBuilder(mocker)
                .BuildForSupplier()
                .WithSupplier(supplier1)
                .WithSupplier(supplier2)
                .WithInvoices(invoices)
                .Create();

            // Act
            var result = target.GetTotalSpend(supplier1.Id);

            // Assert
            result.Years.Count.Should().Be(2);
            result.Years.Single(y => y.Year == 2022).TotalSpend.Should().Be(200);
            result.Years.Single(y => y.Year == 2023).TotalSpend.Should().Be(200);
        }

        [Fact]
        public void GetTotalSpend_OneExternalSupplier_GetsSummarizedSuccessfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var externalSupplier = new SupplierBuilder()
                .AsExternalSupplier()
                .Create();
            var invoices = new InvoiceBuilder()
                .AddInvoice(2022, externalSupplier.Id, 100)
                .AddInvoice(2022, externalSupplier.Id, 100)
                .Create();
            var target = new SpendServiceBuilder(mocker)
                .BuildForExternalSupplier()
                .WithSupplier(externalSupplier)
                .WithExternalInvoices(invoices, externalSupplier.Id)
                .Create();

            // Act
            var result = target.GetTotalSpend(externalSupplier.Id);

            // Assert
            result.Years.Count.Should().Be(1);
            result.Years.First().Year.Should().Be(2022);
            result.Years.First().TotalSpend.Should().Be(200);
        }

        [Fact]
        public void GetTotalSpend_TwoExternalSuppliers_OnlyOneGetsSummarizedSuccessfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var externalSupplier1 = new SupplierBuilder()
                .AsExternalSupplier()
                .Create();
            var externalSupplier2 = new SupplierBuilder()
                .AsExternalSupplier()
                .Create();
            var invoices1 = new InvoiceBuilder()
                .AddInvoice(2022, externalSupplier1.Id, 100)
                .AddInvoice(2022, externalSupplier1.Id, 100)
                .Create();
            var invoices2 = new InvoiceBuilder()
                .AddInvoice(2022, externalSupplier2.Id, 100)
                .AddInvoice(2022, externalSupplier2.Id, 100)
                .Create();
            var target = new SpendServiceBuilder(mocker)
                .BuildForExternalSupplier()
                .WithSupplier(externalSupplier1)
                .WithSupplier(externalSupplier2)
                .WithExternalInvoices(invoices1, externalSupplier1.Id)
                .WithExternalInvoices(invoices2, externalSupplier2.Id)
                .Create();

            // Act
            var result = target.GetTotalSpend(externalSupplier1.Id);

            // Assert
            result.Years.Count.Should().Be(1);
            result.Years.First().Year.Should().Be(2022);
            result.Years.First().TotalSpend.Should().Be(200);
        }
    }
}