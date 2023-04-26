using ProArch.CodingTest.Exceptions;
using ProArch.CodingTest.Invoices.Repository;
using System;
using System.Collections.Generic;

namespace ProArch.CodingTest.Invoices.Service.External
{
    public class FailoverInvoiceService
    {
        private readonly IFailoverInvoiceRepository _failoverInvoiceRepository;
        private readonly FailoverInvoiceOptions _failoverInvoiceOptions;

        public FailoverInvoiceService(IFailoverInvoiceRepository failoverInvoiceRepository, FailoverInvoiceOptions failoverInvoiceOptions)
        {
            _failoverInvoiceRepository = failoverInvoiceRepository;
            _failoverInvoiceOptions = failoverInvoiceOptions;
        }

        public IEnumerable<Invoice> GetInvoices(int supplierId)
        {
            var failoverInvoices = _failoverInvoiceRepository.GetBySupplier(supplierId);
            if (failoverInvoices?.Invoices is null or { Length: 0 })
            {
                throw new EntityNotFoundException($"Failover invoices for supplier {supplierId} not found.");
            }
            if (DateTime.UtcNow.AddMonths(-1 * _failoverInvoiceOptions.ValidBeforeMonthsOld) > failoverInvoices.Timestamp)
            {
                throw new StaleFailoverInvoicesException($"Failover invoices for supplier {supplierId} are stale.");
            }
            return failoverInvoices.Map(supplierId);
        }
    }
}
