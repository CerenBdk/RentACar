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
    public class ColorManager:IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public void Add(Color entity)
        {
            _colorDal.Add(entity);
        }

        public void Update(Color entity)
        {
            _colorDal.Update(entity);
        }

        public void Delete(Color entity)
        {
            _colorDal.Delete(entity);
        }

        public Color Get(Color entity)
        {
            return _colorDal.Get(x => x.ID == entity.ID);
        }

        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public void GetList(List<Color> list)
        {
            Console.WriteLine("\n------- Color List -------");
            int count = 1;
            foreach (var color in list)
            {
                Console.WriteLine("{0}- ID: {1}\n   Color Name: {2}", count, color.ID, color.Name);
                count++;
            }
        }

        public Color FindByID(int Id)
        {
            Color c = new Color();
            if (_colorDal.GetAll().Any(x => x.ID == Id))
            {
                c = _colorDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such color was found.");
            return c;
        }
    }
}
