
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TourismDataAccess;

namespace TourismRepository.Repository
{
    class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet; 

        private dbTourismEventAppEntities _tourismEntities;

        public GenericRepository(dbTourismEventAppEntities tourismEntities)
        {
            _tourismEntities = tourismEntities;
        }

        public IQueryable<T> GetAllRecordsQuerable => _dbSet;

        public void Delete(T entity)
        {
            if (_tourismEntities.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public T Get(int? id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public void Save(T entity)
        {
            _dbSet.Add(entity);
            _tourismEntities.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _tourismEntities.Entry(entity).State = EntityState.Modified;
            _tourismEntities.SaveChanges();
        }
    }
}
