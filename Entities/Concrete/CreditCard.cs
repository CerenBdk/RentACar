using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard :IEntity
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}
