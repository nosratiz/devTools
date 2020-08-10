using System;
using AutoMapper;
using DevTools.Application.Users.Command.CreateUser;
using DevTools.Application.Users.Command.UpdateUser;
using DevTools.Application.Users.Model;
using DevTools.Domain.Models;

namespace DevTools.Application.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.RoleName, opt => opt.MapFrom(des => des.Role.Name));

            CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.ExpiredDate, opt => opt.MapFrom(des => DateTime.Now.AddDays(2)))
                .ForMember(x => x.RegisterDate, opt => opt.MapFrom(des => DateTime.Now))
                .ForMember(x => x.IsEmailConfirm, opt => opt.MapFrom(des => true))
                .ForMember(x => x.IsMobileConfirm, opt => opt.MapFrom(des => !string.IsNullOrWhiteSpace(des.Mobile)))
                .ForMember(x => x.ConfirmCode, opt => opt.MapFrom(des => Guid.NewGuid().ToString("N")))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => Guid.NewGuid()));


            CreateMap<UpdateUserCommand, User>();
        }
    }
}