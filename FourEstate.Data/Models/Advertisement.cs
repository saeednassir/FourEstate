using FourEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data.Models
{
    public class Advertisement : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string WebsiteUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public ContentStatus Stauts { get; set; }

        public Advertisement()
        {
            Stauts = ContentStatus.Pending;
        }
    }
}
