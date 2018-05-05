using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOSBB.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public string FlatNumber { get; set; }
        public ApplicationUser User { get; set; }
        public decimal Sum { get; set; }
    }
}
