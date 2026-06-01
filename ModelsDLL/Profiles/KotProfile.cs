using ModelsDLL.Models;
using AutoMapper;
using ModelsDLL.DTO;

namespace ModelsDLL.Profiles {
    public class KotProfile : Profile {
        public KotProfile() {
            CreateMap<KotStudentDTO, Student>()
                .ForMember(dest => dest.Matricule, opt => opt.MapFrom(src => src.ETU_MATRICULE))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ETU_NOM))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ETU_PRENOM));

            CreateMap<KotStudentDTO, Kot>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.KOT_ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.KOT_NAME))
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src));

        }

    }
}
