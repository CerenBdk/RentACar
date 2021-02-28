using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result :IResult
    {
        private bool _success;
        private string _message;

        public Result(bool success, string message):this(success)
        {
            this._message = message;
        }

        public Result(bool success)
        {
            this._success = success;
        }

        public bool Success
        {
            get { return this._success; }
        }

        public string Message
        {
            get { return this._message; }
        }
    }
}
