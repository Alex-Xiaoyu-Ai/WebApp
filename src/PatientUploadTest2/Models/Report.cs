using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientUploadTest2.Models


{
    public enum ReportState
    {
        Approved, Written, Rejected, Unwritten

    }

    

    public class Report
    {
        [Key]
        [Display(Name ="报告编号")]
        public int Id { get; set; }

        
        [Display(Name ="撰写人")]
        public String Author { get; set; }

        [Display(Name ="审核人")]
        public String Auditor { get; set; }

        [Display(Name ="所在医院")]
        public String HospitalClient { get; set; }

        [Display(Name ="撰写时间")]
        public DateTime WritingTime { get; set; }

        [Display(Name ="审核时间")]
        public DateTime PublishingTime { get; set; }

        [Display(Name = "报告状态")]
        public ReportState state { get; set; }

        [Display(Name ="检查所见")]
        public String Observation { get; set; }

        [Display(Name ="诊断意见")]
        public String Diagnosis { get; set; }

        
        public virtual Patient Patient { get; set; }


    }
}
