using MyOSBB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOSBB.DAL.Models
{
    class UnitOfWork : IUnitOfWork
    {
        public void Save()
        {
            //_bloggingContext.SaveChanges();
        }
    }
}
