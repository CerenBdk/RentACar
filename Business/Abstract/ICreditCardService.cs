using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService : IEntityService<CreditCard>
    {
        IDataResult<List<CreditCard>> GetAllCreditCardByCustomerId(int customerId);
        IResult DeleteById(int cardId);
    }
}
