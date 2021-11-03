using FourEstate.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Dtos
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المستخدم")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Phone]
        [Display(Name = "رقم الجوال ")]
        public string PhoneNumber { get; set; }
        [Display(Name = "الصورة")]
        public IFormFile Image { get; set; }
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "نوع المستخدم")]
        public UserType UserType { get; set; }
    }
}
