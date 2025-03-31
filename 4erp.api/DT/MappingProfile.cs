using AutoMapper;
using _4erp.api.DT.Vacancies;
using _4erp.api.entities.vacancy;
using _4erp.domain.VO;
using _4erp.api.entities.ocupation;
using _4erp.api.entities.skill;
using _4erp.api.entities.candidature;
using _4erp.api.entities.status;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CandidatureDT, Candidature>()
        .ForMember(dest => dest.Vacancy, opt => opt.MapFrom(src =>
            src.Vacancy != null ? new Vacancy { Id = src.Vacancy.Id } : null)).ReverseMap();

        CreateMap<VacancyDT, Vacancy>()
            .ForMember(dest => dest.Ocupation, opt => opt.MapFrom(src =>
                src.Ocupation != null ? new Ocupation { Id = src.Ocupation.Id } : null))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.Status != null ? new Status { Id = src.Status.Id } : null)) 
            .ForMember(dest => dest.OcupationId, opt => opt.MapFrom(src => src.Ocupation != null  ?  src.Ocupation.Id : Guid.Empty ))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src =>
                src.Skills != null ? src.Skills.Select(s => new Skill { Id = Guid.Parse(s) }).ToList() : new List<Skill>())).ReverseMap();
    }
}