using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService:IEntityService<Customer>
    {
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
        IDataResult<CustomerDetailDto> GetCustomerDetailByMail(string mail);
    }
}
