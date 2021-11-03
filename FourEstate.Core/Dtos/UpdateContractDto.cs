using FourEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Dtos
{
   public class UpdateContractDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "نوع العقد")]
        public ContractType ContractType { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "السعر")]
        public double Price { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الزبون")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "العقار")]
        public int RealEstatedId { get; set; }
    }
}
