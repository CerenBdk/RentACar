using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager: ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal; 
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult("Car" + Messages.AddSingular);
        }

        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult("Car" + Messages.UpdateSingular);
        }

        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult("Car" + Messages.DeleteSingular);
        }

        public IDataResult<Car> Get(Car entity)
        {
            return new SuccessDataResult<Car>(_carDal.Get(x => x.ID == entity.ID));
        }

        public IDataResult<List<Car>> GetCarsByBrandID(int Id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.BrandID == Id));
        }

        public IDataResult<List<Car>> GetCarsByColorID(int Id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.ColorID == Id));
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.DailyPrice >= min && x.DailyPrice <= max));
        }

        public IResult GetList(List<Car> list)
        {
            Console.WriteLine("\n------- Car List -------");

            foreach (var car in list)
            {
                Console.WriteLine("{0}- Brand ID: {1}\n    Color ID: {2}\n    Name: {3}\n    Model Year: {4}\n    Daily Price: {5} TL\n    Description: {6}\n",car.ID, car.BrandID, car.ColorID, car.Name, car.ModelYear, car.DailyPrice, car.Description);
            }
            return new SuccessResult();
        }

        public IDataResult<Car> FindByID(int Id)
        {
            Car c = new Car();
            if (_carDal.GetAll().Any(x => x.ID == Id))
            {
                c = _carDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such car was found.");
            return new SuccessDataResult<Car>(c);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

    }
}
