using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismRepository.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllRecords();
        IQueryable<T> GetAllRecordsQuerable { get; }
        T Get(int? id);
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
