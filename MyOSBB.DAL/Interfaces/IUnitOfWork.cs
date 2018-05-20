using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Models.Invoices;
using System;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Announcement> Announcements { get; }
        IRepository<Contribution> Contributions { get; }
        IRepository<Month> Months { get; }
        IRepository<InvoiceElectro> InvoiceElectroes { get; }
        IRepository<InvoiceGaz> InvoiceGazs { get; }

        EntityEntry<T> Entry<T>(T item) where T : class;
        EntityEntry<T> Add<T>(T item) where T : class;
        EntityEntry<T> Update<T>(T item) where T : class;
        EntityEntry<T> Remove<T>(T item) where T : class;
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
