using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ProArch.CodingTest.Exceptions;
using ProArch.CodingTest.Invoices;
using ProArch.CodingTest.Invoices.Repository;
using ProArch.CodingTest.Invoices.Service.External;
using ProArch.CodingTest.UnitTests.Builders;

namespace ProArch.CodingTest.UnitTests
{
    public class FailoverInvoiceServiceTests
    {
        [Fact]
        public void GetFreshInvoices_Successfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var options = new FailoverInvoiceOptions
            {
                ValidBeforeMonthsOld = 1
            };
            mocker.Use(options);
            var externalInvoices = new ExternalInvoiceBuilder()
                .AddExternalInvoice(2001, 121)
                .WithFreshFailoverTimestamp()
                .CreateFailoverInvoiceCollection();
            mocker
                .GetMock<IFailoverInvoiceRepository>()
                .Setup(m => m.GetBySupplier(It.IsAny<int>()))
                .Returns(externalInvoices);
            var target = mocker.CreateInstance<FailoverInvoiceService>();

            // Act
            var result = target.GetInvoices(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(externalInvoices.Invoices.Count());
            result.First().InvoiceDate.Year.Should().Be(2001);
            result.First().Amount.Should().Be(121);
        }

        [Fact]
        public void NoInvoicesForSupplier_Throws()
        {
            // Arrange
            var mocker = new AutoMocker();
            var options = new FailoverInvoiceOptions
            {
                ValidBeforeMonthsOld = 1
            };
            mocker.Use(options);
            mocker
                .GetMock<IFailoverInvoiceRepository>()
                .Setup(m => m.GetBySupplier(It.IsAny<int>()))
                .Returns((FailoverInvoiceCollection)null!);
            var target = mocker.CreateInstance<FailoverInvoiceService>();

            // Act
            Action act = () => target.GetInvoices(1);

            // Assert
            act.Should().Throw<EntityNotFoundException>();
        }

        [Fact]
        public void StaleInvoices_Throws()
        {
            // Arrange
            var mocker = new AutoMocker();
            var options = new FailoverInvoiceOptions
            {
                ValidBeforeMonthsOld = 0
            };
            mocker.Use(options);
            var externalInvoices = new ExternalInvoiceBuilder()
                .AddExternalInvoice(2001, 121)
                .WithStaleFailoverTimestamp()
                .CreateFailoverInvoiceCollection();
            mocker
                .GetMock<IFailoverInvoiceRepository>()
                .Setup(m => m.GetBySupplier(It.IsAny<int>()))
                .Returns(externalInvoices);
            var target = mocker.CreateInstance<FailoverInvoiceService>();

            // Act
            Action act = () => target.GetInvoices(1);

            // Assert
            act.Should().Throw<StaleFailoverInvoicesException>();
        }
    }
}
