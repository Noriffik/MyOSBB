using System;

namespace MyOSBB.DAL.Models
{
    public class ContributionApi
    {
        public string FlatNumber { get; set; }
        public string UserName { get; set; }
        public string Payment { get; set; }
        public DateTime PaymentDate { get; set; }
        public string MonthName { get; set; }
    }
}
