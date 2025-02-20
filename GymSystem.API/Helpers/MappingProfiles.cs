using AutoMapper;
using GymSystem.BLL.Dtos.Trainer;
using GymSystem.DAL.Entities.Identity;

namespace GymSystem.API.Helpers
{
	public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            // Map from AppUser to TrainerDto
            CreateMap<AppUser, TrainerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.IsStopped, opt => opt.MapFrom(src => src.IsStopped))
                .ForMember(dest => dest.HaveDays, opt => opt.MapFrom(src => src.HaveDays))
                .ForMember(dest => dest.AddBy, opt => opt.MapFrom(src => src.AddBy))
                .ForMember(dest => dest.RemainingDays, opt => opt.MapFrom(src => src.RemainingDays));


            // Map from CreateTrainerDto to AppUser
            CreateMap<CreateTrainerDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => 2)); // Trainer Role

            // Map from UpdateTrainerDto to AppUser
            CreateMap<UpdateTrainerDto, AppUser>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));
            //====================================================================================

        }
    }
}
