using System;

namespace OLSoftware.BL.DTOs
{
    public class ReportDTO
    {
        public string CustomerName { get; set; }
        public string CustomerTelephone { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public double? ProjectPrice { get; set; }
        public double? ProjectHours { get; set; }
        public string ProjectStateName { get; set; }
    }
}
