using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientUploadTest2.Models
{
    public class ReportTemplate
    {
        public int id { get; set; }
        public String Study { get; set; }
        public String Observation { get; set; }
        public String Diagnosis { get; set; }
        public String Remark { get; set; }
    }
}
