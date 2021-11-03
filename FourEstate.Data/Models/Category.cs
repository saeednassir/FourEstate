using FourEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data.Models
{
    public class Category :BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RealEstate> RealEstates { get; set; }

       
   
    }
}
