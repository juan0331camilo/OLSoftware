using AutoMapper;
using OLSoftware.BL.Models;

namespace OLSoftware.BL.DTOs
{
    public class Mapper : Profile
    {
        //  Model (DB) -> DTO
        //  DTO -> Model (DB)
        public Mapper()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectState, ProjectStateDTO>().ReverseMap();
            CreateMap<ProjectLanguage, ProjectLanguageDTO>().ReverseMap();
        }
    }
}
