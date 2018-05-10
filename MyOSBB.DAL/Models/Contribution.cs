using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Models
{
    public enum Month { January, February, March, April, May, June, July, August, September, October, November, December }
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
        public Month ForPeriod { get; set; }

        public ApplicationUser User { get; set; }

        //public virtual IEnumerable<(int Id, Month Value)> Months => MonthList.GetMonthList();
        //public virtual SelectList SelectMonthList { get { return new SelectList(Months, "Id", "Value", 0); } }
    }
}
