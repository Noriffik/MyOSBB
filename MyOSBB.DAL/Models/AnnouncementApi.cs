using System;

namespace MyOSBB.DAL.Models
{
    public class AnnouncementApi
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
