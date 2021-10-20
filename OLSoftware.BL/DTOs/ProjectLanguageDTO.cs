namespace OLSoftware.BL.DTOs
{
    public partial class ProjectLanguageDTO
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? LanguageId { get; set; }
        public int? Level { get; set; }

        public virtual LanguageDTO Language { get; set; }
        public virtual ProjectDTO Project { get; set; }
    }
}
