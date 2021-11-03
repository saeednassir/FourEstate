using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.ViewModels
{
    public class RealEstateViewModel
    {
        public int Id { get; set; }
      
        public string Name { get; set; }

        public string Description { get; set; }

        public string RealEstateType { get; set; }
       
        public string Status { get; set; }

        public LocationViewModel Location { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<RealEstateAttachmentViewModel> Attachments { get; set; }

    }
}
