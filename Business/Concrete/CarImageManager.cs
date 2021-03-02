using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        private readonly string DefaultImage = Environment.CurrentDirectory + @"\wwwroot\Images\default.jpg";
        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }
        public IResult Add(IFormFile image, CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfCarIsExists(img.CarID), 
                                           CheckIfFileExtensionValid(image.FileName),
                                           CheckIfImageNumberLimitForCar(img.CarID));

            if (result != null)
            {
                return result;
            }
            img.ImagePath = FileOperationsHelper.Add(image);
            img.CreatedAt = DateTime.Now;            
            _carImageDal.Add(img);
            return new SuccessResult("Image" + Messages.AddSingular);
        }

        public IResult Update(IFormFile image, CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsExists(img.ID),
                                           CheckIfFileExtensionValid(image.FileName));
            if (result != null)
            {
                return result;
            }
            var carImg = _carImageDal.Get(x => x.ID == img.ID);
            carImg.CreatedAt = DateTime.Now;
            carImg.ImagePath = FileOperationsHelper.Add(image);
            FileOperationsHelper.Delete(img.ImagePath);
            _carImageDal.Update(carImg);
            return new SuccessResult("Image" + Messages.UpdateSingular);
        }

        public IResult Delete(CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfImagePathIsExists(img.ImagePath));
            if (result != null)
            {
                return result;
            }
            
            _carImageDal.Delete(img);
            FileOperationsHelper.Delete(img.ImagePath);
            return new SuccessResult("Image" + Messages.DeleteSingular);
        }

        public IDataResult<CarImage> FindByID(int Id)
        {
            CarImage img = new CarImage();
            if (_carImageDal.GetAll().Any(x => x.ID == Id))
            {
                img = _carImageDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine("No such car image was found.");
            return new SuccessDataResult<CarImage>(img);
        }

        public IDataResult<CarImage> Get(CarImage img)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(x => x.ID == img.ID));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IResult GetList(List<CarImage> list)
        {
            Console.WriteLine("\n------- Car Image List -------");

            foreach (var img in list)
            {
                Console.WriteLine("{0}- Car ID: {1}\n    Image Path: {2}\n    CratedAt: {3}\n", img.ID, img.CarID, img.ImagePath, img.CreatedAt);
            }
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetCarListByCarID(int carID)
        {
            if (!_carImageDal.GetAll().Any(x => x.CarID == carID))
            {
                List<CarImage> img = new List<CarImage>
                {
                    new CarImage
                    {
                        CarID = carID,
                        ImagePath = DefaultImage
                    }
                };
                return new SuccessDataResult<List<CarImage>>(img);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(x => x.CarID == carID)); 
        }

        private IResult CheckIfCarIsExists(int carId)
        {
            if (!_carService.GetAll().Data.Any(x => x.ID == carId))
            {
                return new ErrorResult(Messages.NotExist + "car");
            }
            return new SuccessResult();
        }

        private IResult CheckIfFileExtensionValid(string file)
        {
            if (!Regex.IsMatch(file, @"([A-Za-z0-9\-]+)\.(png|PNG|gif|GIF|jpg|JPG|jpeg|JPEG)"))
            {
                return new ErrorResult(Messages.InvalidFileExtension);
            }

            return new SuccessResult();
        }

        private IResult CheckIfImagePathIsExists(string path)
        {
            if (!_carImageDal.GetAll().Any(x => x.ImagePath == path))
            {
                return new ErrorResult(Messages.NotExist + "image");
            }

            return new SuccessResult();
        }

        private IResult CheckIfImageNumberLimitForCar(int carId)
        {
            if (_carImageDal.GetAll(x => x.CarID == carId).Count == 5)
            {
                return new ErrorResult(Messages.ImageNumberLimitExceeded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageIsExists(int Id)
        {
            if (!_carImageDal.GetAll().Any(x => x.ID == Id))
            {
                return new ErrorResult(Messages.NotExist + "image");
            }
            return new SuccessResult();
        }
    }
}
