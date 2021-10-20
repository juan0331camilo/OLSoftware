using System;
using System.Collections.Generic;

#nullable disable

namespace OLSoftware.BL.Models
{
    public partial class ProjectState
    {
        public ProjectState()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
