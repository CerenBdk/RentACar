using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEntityService<T> where T: class
    {
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(T entity);
        void GetList(List<T> list);
        T FindByID(int Id);
    }
}
