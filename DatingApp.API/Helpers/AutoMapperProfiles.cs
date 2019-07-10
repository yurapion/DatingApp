using System;
using System.Linq;
using AutoMapper;
using DatingApp.API.DTO;
using DatingApp.API.Model;

namespace DatingApp.API.Helpers
{
    // We Created a new class to tell automaper what type he must support
    public class AutoMapperProfiles : Profile
    {  
        public AutoMapperProfiles()
        {
            //ForMember - specify the way how to get value to prop PhotoUrl in UserForListDto
            // Specify the way of calculating age
            // CalculateAge is our own method we created in extension class
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });
            // .ForMember(dest => dest.Age, opt => {
            //     opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year);
            // });

            CreateMap<User, UserForDetailDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });
           

            CreateMap<Photo, PhotosForDetailsDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
        }
    }
}