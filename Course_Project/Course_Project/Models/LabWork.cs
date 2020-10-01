using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Course_Project.Models
{
    public class LabWork
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Name")]
        [RegularExpression(@"^[A-Z0-9]+[0-9a-zA-Z'\s-]*$")]
         public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Type")]
        [RegularExpression(@"^[A-Z]+[a-z'\s-]*$")]
        public string Type { get; set; }

        public ICollection<Passing> Passings { get; set; }
        public ICollection<StudentLabWork> StudentLabWork { get; set; }
    }
}
