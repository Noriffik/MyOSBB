using System;
using System.ComponentModel.DataAnnotations;

namespace MyOSBB.DAL.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
