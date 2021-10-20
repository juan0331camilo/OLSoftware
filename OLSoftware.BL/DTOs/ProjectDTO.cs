using System;
using System.ComponentModel.DataAnnotations;

namespace OLSoftware.BL.DTOs
{
    public partial class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Price { get; set; }
        public double? Hours { get; set; }

        [Display(Name = "Project State")]
        public int? ProjectStateId { get; set; }

        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        public virtual ProjectStateDTO ProjectState { get; set; }        
    }
}
