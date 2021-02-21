using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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
      
        public IResult Add(Brand entity)
        {
            _brandDal.Add(entity);
            return new SuccessResult("Brand" + Messages.AddSingular);
        }

        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult("Brand" + Messages.UpdateSingular);
        }

        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            return new SuccessResult("Brand" + Messages.DeleteSingular);
        }

        public IDataResult<Brand> Get(Brand entity)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(x => x.ID == entity.ID));
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IResult GetList(List<Brand> list)
        {
            Console.WriteLine("\n------- Brand List -------");
            foreach (var brand in list)
            {
                Console.WriteLine("{0}- Brand Name: {1}", brand.ID, brand.Name);
            }
            return new SuccessResult();
        }

        public IDataResult<Brand> FindByID(int Id)
        {
            Brand b = new Brand();
            if (_brandDal.GetAll().Any(x => x.ID == Id))
            {
                b = _brandDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such brand was found.");
            return new SuccessDataResult<Brand>(b);
        }

    }
}
