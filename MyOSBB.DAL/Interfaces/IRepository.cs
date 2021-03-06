﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyOSBB.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        DbSet<TEntity> GetDbSet();
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
