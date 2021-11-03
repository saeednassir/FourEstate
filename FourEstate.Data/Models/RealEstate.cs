using FourEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data.Models
{
   public  class RealEstate :BaseEntity
    
    {

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public RealEstateType RealEstateType { get; set; }

        public ContentStatus Stauts { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public List<RealEstatetAttachment> Attachments { get; set; }

        public List<Contract> Contracts{ get; set; }



        public RealEstate()
        {
            Stauts = ContentStatus.Pending;
        }

    }
}
