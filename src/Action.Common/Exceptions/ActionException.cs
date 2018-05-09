using System;
using System.Collections.Generic;
using System.Text;

namespace Action.Common.Exceptions
{
    public class ActionException : Exception
    {
        public string Code { get; set; }
        public ActionException()
        {

        }

        public ActionException(string code) => Code = code;

        public ActionException(string code, string message) : base(message) {
            Code = code;
        }
        //public ActionException(Exception innerException, string message, params object[] args) : this(innerException, string.Empty, message, args) { }

        //public ActionException(string code, string message, params object[] args) : this(code, message, args) {

        //    Code = code;

        //}
    }
}
