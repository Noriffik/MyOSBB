using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOSBB.DAL.Models
{
    //public enum Month { January, February, March, April, May, June, July, August, September, October, November, December }

    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
