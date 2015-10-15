using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLModel.Exceptions
{
    public class DALAlreadyExistsException : Exception
    {
        public string Err { get; private set; }

        public DALAlreadyExistsException(string err = null)
        {
            Err = err;
        }
    }

    public class DALOutOfRangeException : Exception
    {
        public string Err { get; private set; }

        public DALOutOfRangeException(string err = null)
        {
            Err = err;
        }
    }

    public class DALInfoNotSpecifiedException : Exception
    {
        public string Err { get; private set; }

        public DALInfoNotSpecifiedException(string err = null)
        {
            Err = err;
        }
    }
}