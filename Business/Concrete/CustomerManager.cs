using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager:ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager (ICustomerDal customerDal)
	    {
            _customerDal = customerDal;
	    }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IResult Add(Customer entity)
        {
            _customerDal.Add(entity);
            return new SuccessResult("Customer" + Messages.AddSingular);
        }

        public IResult Update(Customer entity)
        {
            _customerDal.Update(entity);
            return new SuccessResult("Customer" + Messages.UpdateSingular);
        }

        public IResult Delete(Customer entity)
        {
            _customerDal.Delete(entity);
            return new SuccessResult("Customer" + Messages.DeleteSingular);
        }

        public IDataResult<Customer> Get(Customer entity)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(x => x.UserID == entity.UserID));
        }

        public IResult GetList(List<Customer> list)
        {
            Console.WriteLine("\n------- Customer List -------");
            int counter = 1;
            foreach (var c in list)
            {
                Console.WriteLine("{0}- Customer ID: {1}\n   Company Name: {2}\n", counter, c.UserID, c.CompanyName);
                counter++;
            }
            return new SuccessResult();
        }

        public IDataResult<Customer> FindByID(int Id)
        {
            Customer c = new Customer();
            if (_customerDal.GetAll().Any(x => x.UserID == Id))
            {
                c = _customerDal.GetAll().FirstOrDefault(x => x.UserID== Id);
            }
            else Console.WriteLine(Messages.NotExist + "Customer");
            return new SuccessDataResult<Customer>(c);
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }
    }
}
