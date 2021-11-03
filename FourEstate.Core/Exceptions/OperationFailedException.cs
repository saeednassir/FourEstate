using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException() : base("Operation Failed")
        {

        }
    }
}
