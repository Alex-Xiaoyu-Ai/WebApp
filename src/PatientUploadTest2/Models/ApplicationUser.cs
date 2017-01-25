using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PatientUploadTest2.Models
{

    public enum Roles
    {

        SuperUser, Doctor, Auditor, HospitalClient,  
                    
    }
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "姓名")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "职位")]
        public Roles  Role { get; set; }

        [Required]
        [Display(Name ="工作单位")]
        public String Employer { get; set; }

        [Display(Name ="签名")]
        public String SigniturePath { get; set; }

        //public ICollection<Report> PatientReports { get; set; }
    }
}
