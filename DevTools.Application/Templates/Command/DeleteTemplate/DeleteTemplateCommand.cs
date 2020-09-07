using System;
using DevTools.Application.Templates.Dto;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Templates.Command.DeleteTemplate
{
    public class DeleteTemplateCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}