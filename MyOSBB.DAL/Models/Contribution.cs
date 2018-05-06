using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public string FlatNumber { get; set; }
        public string UserId { get; set; }
        public string Payment { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ForPeriod { get; set; }
    }
}
