using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم التصنيف")]
        public string Name { get; set; }
    }
}
