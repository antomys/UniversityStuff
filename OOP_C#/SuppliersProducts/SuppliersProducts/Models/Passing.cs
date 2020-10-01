using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliersProducts.Models
{
    public class Passing
    {
        public int ID { get; set; }
        public int LabWorkID { get; set; }
        public int TeacherID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string Place { get; set; }

        public LabWork LabWork { get; set; }
        public Teacher Teacher { get; set; }

    }
}
