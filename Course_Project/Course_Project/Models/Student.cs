using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course_Project.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "FullName")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string FullName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Group")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string Group { get; set; }

        [Required]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<StudentLabWork> StudentLabWork { get; set; }
    }
}
