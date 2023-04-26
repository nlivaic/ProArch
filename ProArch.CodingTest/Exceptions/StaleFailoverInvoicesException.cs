using System;

namespace ProArch.CodingTest.Invoices
{
    public class StaleFailoverInvoicesException : Exception
    {
        public StaleFailoverInvoicesException(string message)
            : base(message)
        {
        }
    }
}
