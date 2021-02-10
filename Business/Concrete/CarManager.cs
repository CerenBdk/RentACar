using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public void Add(Car entity)
        {
            _carDal.Add(entity);
        }

        public void Update(Car entity)
        {
            _carDal.Update(entity);
        }

        public void Delete(Car entity)
        {
            _carDal.Delete(entity);
        }

        public Car Get(Car entity)
        {
            return _carDal.Get(x => x.ID == entity.ID);
        }

        public List<Car> GetCarsByBrandID(int Id)
        {
            return _carDal.GetAll(x => x.BrandID == Id);
        }

        public List<Car> GetCarsByColorID(int Id)
        {
            return _carDal.GetAll(x => x.ColorID == Id);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetByDailyPrice(decimal min, decimal max)
        {
            return _carDal.GetAll(x => x.DailyPrice >= min && x.DailyPrice <= max);
        }

        public void GetList(List<Car> list)
        {
            Console.WriteLine("\n------- Car List -------");
            int count = 1;
            foreach (var car in list)
            {
                Console.WriteLine("{0}- ID: {1}\n    Brand ID: {2}\n    Color ID: {3}\n    Model Year: {4}\n    Daily Price: {5} TL\n    Description: {6}", count, car.ID, car.BrandID, car.ColorID, car.ModelYear, car.DailyPrice, car.Description);
                count++;
            }
        }

        public Car FindByID(int Id)
        {
            Car c = new Car();
            if (_carDal.GetAll().Any(x => x.ID == Id))
            {
                c = _carDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such car was found.");
            return c;
        }
        
         public List<CarDetailDto> GetCarDetails()
        {
            return _carDal.GetCarDetails();
        }
    }
}
