using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager:IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
      
        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }
        public IResult Add(Rental entity)
        {
            _rentalDal.Add(entity);
            var car = _carService.FindByID(entity.CarID).Data;
            car.IsRented = true;
            _carService.Update(car);
            return new SuccessResult("Rental" + Messages.AddSingular);
        }

        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult("Rental" + Messages.UpdateSingular);
        }

        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult("Rental" + Messages.DeleteSingular);
        }

        public IDataResult<Rental> Get(Rental entity)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.ID == entity.ID));
        }

        public IResult GetList(List<Rental> list)
        {
            Console.WriteLine("\n------- Rental List -------");
            foreach (var rental in list)
            {
                Console.WriteLine("{0}- Car ID: {1}\n   Customer ID: {2}\n   Rent Date: {3}\n   Return Date: {4}\n", rental.ID, rental.CarID, rental.CustomerID, rental.RentDate, rental.ReturnDate);
            }
            return new SuccessResult();
        }

        public IDataResult<Rental> FindByID(int Id)
        {
            Rental r = new Rental();
            if (_rentalDal.GetAll().Any(x => x.ID == Id))
            {
                r = _rentalDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine(Messages.NotExist + "rental");
            return new SuccessDataResult<Rental>(r);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }
    }
}
