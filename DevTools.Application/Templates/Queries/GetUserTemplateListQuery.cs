using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetUserTemplateListQuery : IRequest<Result<List<TemplateDto>>>
    {
        public Guid UserId { get; set; }

        public Guid GroupTemplateId { get; set; }
    }


    public class GetUserTemplateListQueryHandler : IRequestHandler<GetUserTemplateListQuery, Result<List<TemplateDto>>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetUserTemplateListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<TemplateDto>>> Handle(GetUserTemplateListQuery request,
            CancellationToken cancellationToken)
        {
            if (!await _context.Users.AnyAsync(x => x.IsDeleted == false && x.Id == request.UserId, cancellationToken))
                return Result<List<TemplateDto>>.Failed(
                    new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            var groupTemplate = await _context.GroupTemplates
                .Include(x => x.Templates)
                .FirstOrDefaultAsync(x =>
                    x.IsDeleted == false && x.Id == request.GroupTemplateId && x.UserId == request.UserId,cancellationToken);

            return Result<List<TemplateDto>>.SuccessFul(_mapper.Map<List<TemplateDto>>(groupTemplate.Templates));
        }
    }
}