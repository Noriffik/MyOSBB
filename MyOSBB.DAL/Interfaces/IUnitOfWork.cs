using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Announcement> Announcements { get; }
        IRepository<Contribution> Contributions { get; }
        IRepository<Month> Months { get; }

        EntityEntry<T> GetEntry<T>(T item) where T : class;
        EntityEntry<T> Update<T>(T item) where T : class;
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
