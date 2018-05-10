using System;
using System.Collections.Generic;
using System.Text;

namespace MyOSBB.DAL.Models
{
    public static class MonthList
    {
        public static IEnumerable<Month> Months { get { return new List<Month>() { Month.January, Month.February, Month.March, Month.April, Month.May, Month.June, Month.July, Month.August, Month.September, Month.October, Month.November, Month.December}; } }
    }
}
