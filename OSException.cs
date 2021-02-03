using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSExceptionLib
{
    public class OSException:Exception
    { 
        public OSException(string errMsg) : base(errMsg)
        {
            //todo log the error in file or else where
        }
    }
}
