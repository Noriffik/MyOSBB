using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Interfaces;
using MyOSBB.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        private Repository<ApplicationUser> usersRepository;
        private Repository<Contribution> contributionRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new Repository<ApplicationUser>(db);
                return usersRepository;
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
