using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        private T _data;
        public DataResult(T data, bool success, string message):base(success,message )
        {

        }
        public DataResult(T data, bool success):base(success)
        {
               this._data = data;
        }
        public T Data 
        { 
            get { return this._data; } 
        }
       
    }
}
