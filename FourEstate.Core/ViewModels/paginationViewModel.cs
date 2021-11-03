using System;
using System.Collections.Generic;
using System.Text;

namespace FourEstate.Core.ViewModel
{
    public class paginationViewModel
    {
        public int NumberOfPages { get; set; }
        public int currentPage { get; set; }
        public Object? Data { get; set; }
    }
}
