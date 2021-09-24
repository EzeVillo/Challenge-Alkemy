using Challenge.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Interfaces
{
    public interface IRepository<T>
        where T : Entities.Entity
    {
        public List<T> GetAllEntities();
        public T Get(int id);
        public T Add(T entity);
        public T Update(T entity);
        public T Delete(int id);
        public IEnumerable<T> FindBy(QueryParameters<T> queryParameters);
    }
}
