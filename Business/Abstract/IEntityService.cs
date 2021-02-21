using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEntityService<T> where T: class
    {
        IDataResult<List<T>> GetAll();
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
        IDataResult<T> Get(T entity);
        IResult GetList(List<T> list);
        IDataResult<T> FindByID(int Id);
    }
}
