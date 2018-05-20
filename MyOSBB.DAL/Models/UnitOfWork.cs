using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Interfaces;
using MyOSBB.DAL.Models.Invoices;
using MyOSBB.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext db;
        private Repository<ApplicationUser> usersRepository;
        private Repository<Announcement> announcementRepository;
        private Repository<Contribution> contributionRepository;
        private Repository<Month> monthRepository;
        private Repository<InvoiceElectro> invoiceElectroRepository;
        private Repository<InvoiceGaz> invoiceGazRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        #region Repositories

        public IRepository<ApplicationUser> Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new Repository<ApplicationUser>(db);
                return usersRepository;
            }
        }

        public IRepository<Announcement> Announcements
        {
            get
            {
                if (announcementRepository == null)
                    announcementRepository = new Repository<Announcement>(db);
                return announcementRepository;
            }
        }

        public IRepository<Contribution> Contributions
        {
            get
            {
                if (contributionRepository == null)
                    contributionRepository = new Repository<Contribution>(db);
                return contributionRepository;
            }
        }

        public IRepository<Month> Months
        {
            get
            {
                if (monthRepository == null)
                    monthRepository = new Repository<Month>(db);
                return monthRepository;
            }
        }

        public IRepository<InvoiceElectro> InvoiceElectroes
        {
            get
            {
                if (invoiceElectroRepository == null)
                    invoiceElectroRepository = new Repository<InvoiceElectro>(db);
                return invoiceElectroRepository;
            }
        }

        public IRepository<InvoiceGaz> InvoiceGazs
        {
            get
            {
                if (invoiceGazRepository == null)
                    invoiceGazRepository = new Repository<InvoiceGaz>(db);
                return invoiceGazRepository;
            }
        }

        #endregion

        public EntityEntry<T> Entry<T>(T item) where T : class
        {
            return db.Entry(item);
        }

        public EntityEntry<T> Add<T>(T item) where T : class
        {
            return db.Add(item);
        }

        public EntityEntry<T> Update<T>(T item) where T : class
        {
            return db.Update(item);
        }

        public EntityEntry<T> Remove<T>(T item) where T : class
        {
            return db.Remove(item);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
