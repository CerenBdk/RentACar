using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        ICustomerService _customerService;

        public UserManager(IUserDal userDal, ICustomerService customerService)
        {
            _userDal = userDal;
            _customerService = customerService;
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult("User" + Messages.AddSingular);
        }

        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult("User" + Messages.UpdateSingular);
        }

        public IResult UpdateUserDto(UserForRegisterDto user, int userId)
        {
            var u = _userDal.GetAll().Where(x => x.ID == userId).FirstOrDefault();
            if (user.FirstName != null && user.FirstName != "" && user.FirstName != " ")
            {
                u.FirstName = user.FirstName;
            }
            if (user.LastName != null && user.LastName != "" && user.LastName != " ")
            {
                u.LastName = user.LastName;
            }
            if (user.Email != null && user.Email != "" && user.Email != " ")
            {
                u.Email = user.Email;
            }
            if (user.Password != null && user.Password != "" && user.Password != " ")
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                u.PasswordHash = passwordHash;
                u.PasswordSalt = passwordSalt;
            }
            _userDal.Update(u);
            return new SuccessResult("User" + Messages.UpdateSingular);
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult("User" + Messages.DeleteSingular);
        }

        public IDataResult<User> Get(User entity)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.ID == entity.ID));
        }

        public IResult GetList(List<User> list)
        {
            Console.WriteLine("\n------- User List -------");
            foreach (var user in list)
            {
                Console.WriteLine("{0}- First Name: {1}\n   Last Name: {2}\n   Email: {3}\n", user.ID, user.FirstName, user.LastName, user.Email);
            }
            return new SuccessResult();
        }

        public IDataResult<User> FindByID(int Id)
        {
            User u = new User();
            if (_userDal.GetAll().Any(x => x.ID == Id))
            {
                u = _userDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine(Messages.NotExist + "user");
            return new SuccessDataResult<User>(u);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<string>> GetUserClaims(int userId)
        {
            var user = _userDal.GetAll().Where(x => x.ID == userId).FirstOrDefault();
            return new SuccessDataResult<List<string>>(_userDal.GetClaims(user).Select(x => x.Name).ToList());
        }
        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.GetAll().Where(u => u.Email == email).FirstOrDefault());
        }

        public IResult CheckIfCustomer(int userId)
        {
            if (!_customerService.GetAll().Data.Any(x => x.UserID == userId))
            {
                return new ErrorResult();               
            }
            return new SuccessResult();
        }
    }
}
