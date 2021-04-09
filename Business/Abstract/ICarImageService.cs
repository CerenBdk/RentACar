using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IResult Add(IFormFile image, CarImage img);
        IResult Update(IFormFile image, CarImage img);
        IResult Delete(CarImage img);
        IDataResult<CarImage> Get(CarImage img);
        IResult GetList(List<CarImage> list);
        IDataResult<CarImage> FindByID(int Id);
        IDataResult<List<CarImage>> GetCarListByCarID(int carID);
    }
}
