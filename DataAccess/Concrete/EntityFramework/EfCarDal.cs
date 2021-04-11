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
    public class EfCarDal : EfEntityRepositoryBase<Car, RecapProjectDBXContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RecapProjectDBXContext context = new RecapProjectDBXContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandID equals b.ID
                             join co in context.Colors
                             on c.ColorID equals co.ID
                             select new CarDetailDto { 
                                 CarID = c.ID, 
                                 CarName = c.Name, 
                                 BrandName = b.Name, 
                                 ColorName = co.Name, 
                                 DailyPrice = c.DailyPrice, 
                                 Description = c.Description, 
                                 IsRented = c.IsRented, 
                                 ModelYear = c.ModelYear,
                                 BrandID = b.ID,
                                 ColorID = co.ID,
                                 MinFindeks =c.MinFindeks,
                                 Thumbnail = c.Thumbnail
                             };
                return result.ToList();
            }
        }
    }
}
