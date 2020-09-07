using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Templates.Dto;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Templates.Queries
{
    public class GetTemplateQuery : IRequest<Result<TemplateDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, Result<TemplateDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetTemplateQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<TemplateDto>> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (template is null)
                return Result<TemplateDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TemplateNotFound)));


            return Result<TemplateDto>.SuccessFul(_mapper.Map<TemplateDto>(template));
        }
    }
}