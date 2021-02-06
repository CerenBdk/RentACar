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
    public class BrandManager:IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand entity)
        {
            _brandDal.Add(entity);
        }

        public void Update(Brand entity)
        {
            _brandDal.Update(entity);
        }

        public void Delete(Brand entity)
        {
            _brandDal.Delete(entity);
        }

        public Brand Get(Brand entity)
        {
            return _brandDal.Get(x => x.ID == entity.ID);
        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public void GetList(List<Brand> list)
        {
            Console.WriteLine("\n------- Brand List -------");
            int count = 1;
            foreach (var brand in list)
            {
                Console.WriteLine("{0}- ID: {1}\n   Brand Name: {2}", count, brand.ID, brand.Name);
                count++;
            }
        }

        public Brand FindByID(int Id)
        {
            Brand b = new Brand();
            if (_brandDal.GetAll().Any(x => x.ID == Id))
            {
                b = _brandDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such brand was found.");
            return b;
        }
    }
}
