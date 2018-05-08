using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        [Display(Name = "Flat number")]
        public string FlatNumber { get; set; }
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        public string Payment { get; set; }
        [Display(Name = "Payment date")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "For period")]
        public string ForPeriod { get; set; }
    }
}
