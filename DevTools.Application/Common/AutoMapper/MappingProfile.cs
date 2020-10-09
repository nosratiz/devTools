using System;
using System.Collections.Generic;
using AutoMapper;
using DevTools.Application.Settings.Command;
using DevTools.Application.Settings.Model;
using DevTools.Application.Templates.Dto;
using DevTools.Application.Tickets.Command.ReplyTicketCommand;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Application.Transactions.Dto;
using DevTools.Application.UserApplications.Dto;
using DevTools.Application.Users.Command.CreateUser;
using DevTools.Application.Users.Command.UpdateUser;
using DevTools.Application.Users.Model;
using DevTools.Common.Enum;
using DevTools.Domain.Models;
using Newtonsoft.Json;

namespace DevTools.Application.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserDto>()
                .ForMember(x => x.RegisterDate, opt => opt.MapFrom(des => des.RegisterDate.ToString("g")))
                .ForMember(x => x.FullName, opt => opt.MapFrom(des => $"{des.Name} {des.Family}"))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(des => des.Role.Name));

            CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.ExpiredDate, opt => opt.MapFrom(des => DateTime.Now.AddDays(2)))
                .ForMember(x => x.RegisterDate, opt => opt.MapFrom(des => DateTime.Now))
                .ForMember(x => x.IsEmailConfirm, opt => opt.MapFrom(des => true))
                .ForMember(x => x.IsMobileConfirm, opt => opt.MapFrom(des => !string.IsNullOrWhiteSpace(des.Mobile)))
                .ForMember(x => x.ConfirmCode, opt => opt.MapFrom(des => Guid.NewGuid().ToString("N")))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => Guid.NewGuid()));


            CreateMap<UpdateUserCommand, User>();


            CreateMap<Ticket, TicketDto>().ForMember(x => x.FullName, opt => opt.MapFrom(des => $"{des.User.Name} {des.User.Family}"));

            CreateMap<ReplyTicketCommand, Ticket>()
                .ForMember(x => x.Id, opt => opt.MapFrom(des => Guid.NewGuid()))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(des => DateTime.Now))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(des => DateTime.Now))
                .ForMember(x => x.TicketPriority, opt => opt.MapFrom(des => TicketPriority.Mediocre))
                .ForMember(x => x.TicketStatus, opt => opt.MapFrom(des => TicketStatus.Open));


            CreateMap<UserApplication, UserApplicationDto>().ForMember(x => x.RestrictIp,
                opt => opt.MapFrom(des => JsonConvert.DeserializeObject<List<string>>(des.RestrictIp)));


            CreateMap<GroupTemplate, GroupTemplateDto>();

            CreateMap<Template, TemplateDto>();

            CreateMap<Transaction, TransactionDto>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(des => $"{des.User.Name} {des.User.Family}"))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.User.Email))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.User.Mobile));

            CreateMap<Template, TemplateListDto>()
                .ForMember(x => x.FullName,
                    opt => opt.MapFrom(des => $"{des.GroupTemplate.User.Name} {des.GroupTemplate.User.Family}"))
                .ForMember(x => x.GroupName, opt => opt.MapFrom(des => des.GroupTemplate.Name));

            CreateMap<UpdateSettingCommand, Setting>();

            CreateMap<Setting, SettingDto>();
        }
    }
}