using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RecapProjectDBContext>,ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var result = from c in context.Car
                             join b in context.Brand
                             on c.BrandID equals b.ID
                             join co in context.Color
                             on c.ColorID equals co.ID
                             select new CarDetailDto { CarID = c.ID, CarName = c.Name, BrandName = b.Name, ColorName = co.Name };
                return result.ToList();
            }
        }
    }
}
