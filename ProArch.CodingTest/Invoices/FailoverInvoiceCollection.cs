using ProArch.CodingTest.External;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Invoices
{
    public class FailoverInvoiceCollection
    {
        public DateTime Timestamp { get; set; }
        public ExternalInvoice[] Invoices { get; set; }

        public FailoverInvoiceCollection()
        {
            this.Invoices = new ExternalInvoice[0];
        }

        public IEnumerable<Invoice> Map(int supplierId) =>
            Invoices.Select(i => new Invoice
            {
                Amount = i.TotalAmount,
                InvoiceDate = new DateTime(i.Year, 1, 1),
                SupplierId = supplierId
            }).AsEnumerable();
    }
}