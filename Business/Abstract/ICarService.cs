using System;
using Entities.Concrete;
using DataAccess.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService:IEntityService<Car>
    {
        List<Car> GetCarsByBrandID(int Id);
        List<Car> GetCarsByColorID(int Id);
        List<Car> GetByDailyPrice(decimal min, decimal max);
    }
}
