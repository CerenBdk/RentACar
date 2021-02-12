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
    public class ColorManager:IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color entity)
        {
            _colorDal.Add(entity);
            return new SuccessResult("Color" + Messages.AddSingular);
        }

        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult("Color" + Messages.UpdateSingular);
        }

        public IResult Delete(Color entity)
        {
            _colorDal.Delete(entity);
            return new SuccessResult("Color" + Messages.DeleteSingular);
        }

        public IDataResult<Color> Get(Color entity)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(x => x.ID == entity.ID));
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IResult GetList(List<Color> list)
        {
            Console.WriteLine("\n------- Color List -------");
            foreach (var color in list)
            {
                Console.WriteLine("{0}- Color Name: {1}", color.ID, color.Name);
            }
            return new SuccessResult();
        }

        public IDataResult<Color> FindByID(int Id)
        {
            Color c = new Color();
            if (_colorDal.GetAll().Any(x => x.ID == Id))
            {
                c = _colorDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such color was found.");
            return new SuccessDataResult<Color>(c);
        }
    }
}
