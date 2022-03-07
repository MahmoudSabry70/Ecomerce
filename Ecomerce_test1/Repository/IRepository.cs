using System.Collections.Generic;

namespace Ecomerce_test1.Repository
{
    public interface IRepository <T>
    {
        int Add(T t);
        int Delete(long id);
        List<T> GetAll();
        T GetById(long id);
        T GetByName(string name);
        int Update(long id, T t);
    }
}
