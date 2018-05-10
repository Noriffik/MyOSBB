using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyOSBB.DAL.Models.Invoices
{
    public class InvoiceWater
    {
        public int Id { get; set; }
        [Display(Name = "Invoice date")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Provider name")]
        public string ProviderName { get; set; }
        public string Payment { get; set; }
        public string Debt { get; set; }
        public string Overpaid { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "For period")]
        public int MonthId { get; set; }
        public Month Month { get; set; }
    }
}
