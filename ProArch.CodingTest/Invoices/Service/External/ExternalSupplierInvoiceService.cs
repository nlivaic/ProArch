using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ProArch.CodingTest.Invoices.Service.External
{
    public class ExternalSupplierInvoiceService : IInvoiceService
    {
        private readonly IExternalInvoiceService _externalInvoiceService;
        private readonly FailoverInvoiceService _failoverInvoiceService;

        public ExternalSupplierInvoiceService(
            IExternalInvoiceService externalInvoiceService,
            FailoverInvoiceService failoverInvoiceService)
        {
            _externalInvoiceService = externalInvoiceService;
            _failoverInvoiceService = failoverInvoiceService;
        }

        public IEnumerable<Invoice> GetInvoices(int supplierId)
        {
            // Implement resilience

            // Option #1 - catch block acts as a fallback:
            //var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreaker(3, TimeSpan.FromMinutes(1));
            //try
            //{
            //    return circuitBreakerPolicy.Execute(() => _externalInvoiceService.GetInvoices(supplierId.ToString());
            //}
            //catch (Exception ex)
            //{
            //    return _failoverInvoiceService.GetInvoices(supplierId.ToString());
            //}

            // Option #2 - fallback expressed through a Polly policy:
            //var circuitBreakerPolicy = Policy<int>.Handle<Exception>().CircuitBreaker(2, TimeSpan.FromMinutes(1));
            //var fallbackPolicy = Policy<int>.Handle<Exception>().Fallback<int>(() => new FailoverFoo().Bar());
            //var wrapPolicy = Policy.Wrap(circuitBreakerPolicy, fallbackPolicy);
            //return wrapPolicy.Execute(() => _externalInvoiceService.GetInvoices(supplierId.ToString()));

            return _externalInvoiceService.GetInvoices(supplierId.ToString());
        }
    }


    public class Foo
    {
        public int Bar()
        {
            throw new Exception("foobar");
        }
    }

    public class FailoverFoo
    {
        public int Bar()
        {
            return 1;
        }
    }

}
