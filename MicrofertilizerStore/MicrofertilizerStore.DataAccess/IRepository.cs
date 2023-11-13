//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Linq.Expressions;
using MicrofertilizerStore.DataAccess.Entities;

namespace MicrofertilizerStore.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
        T? GetById(int id);
        T? GetById(Guid id);
        T Save(T entity);
        void Delete(T entity);
    }
}