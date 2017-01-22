using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientUploadTest2.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致，请重新输入！")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "职位")]
        public Roles Role { get; set; }

        [Required]
        [Display(Name = "工作单位")]
        public String Employer { get; set; }

        [Display(Name = "签名")]
        public String SigniturePath { get; set; }

        public IFormFile SignitureImage { get; set; }


    }
}
