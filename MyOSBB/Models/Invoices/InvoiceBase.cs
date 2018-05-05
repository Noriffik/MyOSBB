using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOSBB.Models.Invoices
{
    public class InvoiceBase
    {
        public int Id { get; set; }
        public string InvoceNumber { get; set; }
        public decimal TotalSum { get; set; }
    }

    public class InvoiceGaz : InvoiceBase
    {
        public int PrevNumber { get; set; }
        public int CurrentNumber { get; set; }
    }

    public class InvoiceElectro : InvoiceBase
    {
        public int PrevNumber { get; set; }
        public int CurrentNumber { get; set; }
    }
}
