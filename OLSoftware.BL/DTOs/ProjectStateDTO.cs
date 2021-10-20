#nullable disable

using System.ComponentModel.DataAnnotations;

namespace OLSoftware.BL.DTOs
{
    public partial class ProjectStateDTO
    {
        public int Id { get; set; }

        [Display(Name = "State Name")]
        public string Name { get; set; }
    }
}
