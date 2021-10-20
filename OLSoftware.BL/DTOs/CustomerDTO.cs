using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OLSoftware.BL.DTOs
{
    public partial class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [JsonProperty("FullName")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
    }
}
