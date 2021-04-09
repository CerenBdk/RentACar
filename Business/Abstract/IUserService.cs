using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService:IEntityService<User>
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<string>> GetUserClaims(int userId);
        IDataResult<User> GetByMail(string email);
        IResult UpdateUserDto(UserForRegisterDto user, int userId);
        IResult CheckIfCustomer(int userId);
    }
}
