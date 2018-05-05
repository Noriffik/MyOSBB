using MyOSBB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOSBB.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Contribution> Contributions { get; }
        void Save();
    }
}
