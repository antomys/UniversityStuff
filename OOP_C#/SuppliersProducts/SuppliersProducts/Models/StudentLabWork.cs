﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliersProducts.Models
{
    public class StudentLabWork
    {
        public int ID { get; set; }

        [Display(Name = "LabWork")]
        public int LabWorkID { get; set; }
        [Display(Name = "Student")]
        public int StudentID { get; set; }

        public LabWork LabWork { get; set; }
        public Student Student { get; set; }

    }
}
