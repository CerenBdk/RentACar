using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCard entity)
        {
            _creditCardDal.Add(entity);
            return new SuccessResult("Credit Card" + Messages.AddSingular);
        }

        public IResult Delete(CreditCard entity)
        {
            _creditCardDal.Delete(entity);
            return new SuccessResult("Credit Card" + Messages.DeleteSingular);
        }

        public IDataResult<CreditCard> FindByID(int Id)
        {
            CreditCard p = new CreditCard();
            if (_creditCardDal.GetAll().Any(x => x.ID == Id))
            {
                p = _creditCardDal.GetAll().FirstOrDefault(x => x.ID == Id);
            }
            else Console.WriteLine(Messages.NotExist + "credit card");
            return new SuccessDataResult<CreditCard>(p);
        }

        public IDataResult<CreditCard> Get(CreditCard entity)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(x => x.ID == entity.ID));
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        public IResult GetList(List<CreditCard> list)
        {
            throw new NotImplementedException();
        }

        public IResult Update(CreditCard entity)
        {
            _creditCardDal.Update(entity);
            return new SuccessResult("Credit Card" + Messages.UpdateSingular);
        }

    }
}
