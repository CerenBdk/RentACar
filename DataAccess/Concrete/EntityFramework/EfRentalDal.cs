using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental, RecapProjectDBContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarID equals c.ID
                             join u in context.User
                             on r.CustomerID equals u.ID
                             select new RentalDetailDto
                             {
                                RentalID = r.ID,
                                CarName = c.Name,
                                CustomerName = u.FirstName + " " + u.LastName,
                                RentDate = r.RentDate,
                                ReturnDate = r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
