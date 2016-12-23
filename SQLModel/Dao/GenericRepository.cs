using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using MVC.Models.Interface;
using System.Linq.Expressions;
using SQLModel.DbContextFactory;
using SQLModel.UnitOfWork;

namespace MVC.Models.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class 
    {
        public IUnitOfWork _UnitOfWork { get; set; }

        public DbContext _context { get; set; }

        private DbSet<TEntity> _dbSet { get; set; }

        public GenericRepository()
        {
            this._context = new ApplicationDbContext();
        }

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this._UnitOfWork = unitOfWork;
            this._context = unitOfWork.Context;
            this._dbSet = unitOfWork.Context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                _context.Set<TEntity>().Add(entity);
                SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
                SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                _context.Entry(entity).State = EntityState.Deleted;
                SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}