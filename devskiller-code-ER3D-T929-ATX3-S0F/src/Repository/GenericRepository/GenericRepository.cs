using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.GenericRepository
{
    public class GenericRepository<T> where T : class
    {
        #region Properties / Attributes
        
        internal DbSet<T> _dbSet;
        protected BlogContext _context;

        #endregion

        #region _CTOR_

        public GenericRepository(BlogContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        #endregion

        #region Public Methods

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Get(object id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Func<T, Boolean> where)
        {
            IQueryable<T> objects = _dbSet.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        #endregion
    }
}