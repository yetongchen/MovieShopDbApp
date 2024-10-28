using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IRepository<T> where T:class
    {
        //CRUD insert, delete, select, update, get count, get by id

        int Insert(T entity);
        int Update(T entity);
        int Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        int GetCount(Expression<Func<T,bool>> filter);
    }
}
