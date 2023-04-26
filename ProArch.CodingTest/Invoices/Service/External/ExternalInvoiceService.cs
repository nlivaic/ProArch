using System;
using System.Collections.Generic;
using System.Linq;
using ExternalInvoiceServiceSDK = ProArch.CodingTest.External.ExternalInvoiceService;

namespace ProArch.CodingTest.Invoices.Service.External
{
    /// <summary>
    /// This is an adapter class, meant to hide our external
    /// dependency (ExternalInvoiceServiceSDK) behind an interface we control.
    /// </summary>
    public class ExternalInvoiceService : IExternalInvoiceService
    {
        public IEnumerable<Invoice> GetInvoices(string supplierId) =>
            ExternalInvoiceServiceSDK
                .GetInvoices(supplierId)
                .Select(i => new Invoice
                {
                    Amount = i.TotalAmount,
                    InvoiceDate = new DateTime(i.Year, 1, 1),
                    SupplierId = Int32.Parse(supplierId)
                }).AsEnumerable();
    }
}
