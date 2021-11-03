using FourEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.ViewModels
{
    public class ContractViewModel
    {
        public int Id { get; set; }
        public string ContractType { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }

        public CustomerViewModel Customer { get; set; }
        public RealEstateViewModel  RealEstate { get; set; }
    }
}
