using System;
using System.Collections.Generic;
using System.Text;

namespace FourEstate.Core.ViewModel
{
    public class APIResponseViewModel
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
