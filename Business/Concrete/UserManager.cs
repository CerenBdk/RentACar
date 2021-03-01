using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
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

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }
    }
}
