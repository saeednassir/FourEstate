using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Dtos
{
    public class CreateLocationDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم الدولة")]
        public string Country { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المدينة")]
        public string City { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم الشارع")]
        public string Street { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم الشارع")]
        public string StreetNumber { get; set; }
    }
}
