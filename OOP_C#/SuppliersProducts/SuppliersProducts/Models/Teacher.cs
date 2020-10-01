using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliersProducts.Models
{
    public class Teacher
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Address")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string Address { get; set; }

        public ICollection<Passing> Passings { get; set; }

    }
}
