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
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src 
                => string.IsNullOrWhiteSpace(src.ETU_MATRICULE) ? null : src));
            // l un ou l autre, ca fait la meme chose
            //CreateMap<KotStudentDTO, Kot>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.KOT_ID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.KOT_NAME))
            //    .ForPath(dest => dest.Student.FirstName, opt => opt.MapFrom(src => src.ETU_PRENOM))
            //    .ForPath(dest => dest.Student.Matricule, opt => opt.MapFrom(src => src.ETU_MATRICULE))
            //    .ForPath(dest => dest.Student.LastName, opt => opt.MapFrom(src => src.ETU_NOM));

        }

    }
}
