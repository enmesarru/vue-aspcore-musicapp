using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class EfRepository<T> 
        : IRepository<T> where T : class, 
            IBaseEntity, new()
    {
        private readonly MusicDbContext _musicDbContext;
        
        public EfRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }

        public void Add(T entity)
        {
            _musicDbContext.Set<T>().Add(entity);
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _musicDbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public int Count()
        {
            return _musicDbContext.Set<T>().Count();
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _musicDbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _musicDbContext.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _musicDbContext.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _musicDbContext.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _musicDbContext.Set<T>().AsEnumerable();
        }

        public T GetSingle(int id)
        {
            return _musicDbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _musicDbContext.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _musicDbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _musicDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            EntityEntry entry = _musicDbContext.Entry<T>(entity);
            entry.State = EntityState.Modified;
        }
    }
}