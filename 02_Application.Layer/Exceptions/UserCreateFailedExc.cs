using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Application.Layer.Exceptions
{
    public class UserCreateFailedExc : Exception
    {
        public UserCreateFailedExc() : base() { }

        public UserCreateFailedExc(string? message) : base(message)
        {
            
        }
        public UserCreateFailedExc(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
