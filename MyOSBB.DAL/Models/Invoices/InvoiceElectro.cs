using System;
using System.Collections.Generic;
using System.Text;

namespace MyOSBB.DAL.Models.Invoices
{
    public class InvoiceElectro
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FlatNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ProviderName { get; set; }
        public string Payment { get; set; }
        public string ForPeriod { get; set; }
        public string Debt { get; set; }
        public string Overpaid { get; set; }

        public string PrevNumber { get; set; }
        public string CurrentNumber { get; set; }
    }
}
