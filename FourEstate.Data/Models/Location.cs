using FourEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data.Models
{
    public class Location :BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public ContentStatus Stauts { get; set; }

        public List<Customer> Customer { get; set; }

        public List<RealEstate> RealEstatess { get; set; }

        public Location()
        {
            Stauts = ContentStatus.Pending;
        }
    }
}
