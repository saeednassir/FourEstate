using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data.Models
{
    public class RealEstatetAttachment
    {
        public int Id { get; set; }
        public int RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
        [Required]
        public string AttachmentUrl { get; set; }

    }
}
