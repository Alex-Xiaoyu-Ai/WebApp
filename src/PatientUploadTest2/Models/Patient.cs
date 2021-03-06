﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PatientUploadTest2.Models
{
    public class Patient
    {
        [Key]
        [Display(Name ="病人编号")]
        public int id { get; set; }

        [Required]
        [Display(Name ="姓名")]
        public String Name { get; set; }

        [Required]
        [Display(Name ="出生日期")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name ="检查项目")]
        public String Study { get; set; }

        public String HistoryPath { get; set; }
        
        public virtual ICollection<Report> Reports { get; set; }

    }
}
