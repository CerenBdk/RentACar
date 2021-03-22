using System;
using Entities.Concrete;
using DataAccess.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface ICarService:IEntityService<Car>
    {
        IDataResult<List<Car>> GetCarsByBrandID(int Id);
        IDataResult<List<Car>> GetCarsByColorID(int Id);
        IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetCarDetails(int id);

        IDataResult<List<CarDetailDto>> GetAllCarDetailsByFilter(int brandId, int colorId);
        IDataResult<List<CarDetailDto>> GetAllCarDetails();
        IResult AddTransactionalTest(Car car);
    }
}
