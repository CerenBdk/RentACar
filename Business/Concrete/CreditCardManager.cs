using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
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
            IResult result = BusinessRules.Run(CheckIfCardIsExists(entity.CreditCardNumber));
            if (result != null)
            {
                return result;
            }
            _creditCardDal.Add(entity);
            return new SuccessResult("Credit Card" + Messages.AddSingular);
        }


        public IResult Delete(CreditCard entity)
        {
            _creditCardDal.Delete(entity);
            return new SuccessResult("Credit Card" + Messages.DeleteSingular);
        }

        public IResult DeleteById(int cardId)
        {
            var card = _creditCardDal.Get(x => x.ID == cardId);
            _creditCardDal.Delete(card);
            return new SuccessResult("Credit Card" + Messages.DeleteSingular);
        }

        public IDataResult<CreditCard> FindByID(int Id)
        {
            CreditCard p = new CreditCard();
            if (!_creditCardDal.GetAll().Any(x => x.ID == Id))
            {
                return new ErrorDataResult<CreditCard>(Messages.NotExist + "credit card");
            }
            p = _creditCardDal.GetAll().FirstOrDefault(x => x.ID == Id);
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

        public IDataResult<List<CreditCard>> GetAllCreditCardByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll().Where(x => x.CustomerID == customerId).ToList());
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

        private IResult CheckIfCardIsExists(string creditCardNumber)
        {
            if (_creditCardDal.GetAll().Any(x => x.CreditCardNumber == creditCardNumber))
            {
                return new ErrorResult(Messages.AlreadyExist + "credit card.");
            }
            return new SuccessResult();
        }

    }
}
