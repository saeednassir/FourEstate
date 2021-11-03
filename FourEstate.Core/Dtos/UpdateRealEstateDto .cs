using FourEstate.Core.Enums;
using FourEstate.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Dtos
{
    public class UpdateRealEstateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم العقار")]
        public string Name { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الموقع")]
        public int LocationId { get; set; }
        //public UpdateLocationDto Location { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التصنيف")]
        public int CategoryId { get; set; }
        //public UpdateCategoryDto Category { get; set; }


        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الصور")]
        public List<IFormFile> Attachments { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "نوع العقار")]
        public RealEstateType RealEstateType { get; set; }
        public List<RealEstateAttachmentViewModel> RealEstateAttachments { get; set; }

    }
}
