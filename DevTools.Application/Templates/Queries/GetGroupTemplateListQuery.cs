using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Templates.Dto;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotImplementedException = System.NotImplementedException;

namespace DevTools.Application.Templates.Queries
{
    public class GetGroupTemplateListQuery : PagingOptions, IRequest<Result<PagedList<GroupTemplateDto>>>
    {
        public Guid UserId { get; set; }
    }

    public class GetGroupTemplateListQueryHandler : PagingService<GroupTemplate>,
        IRequestHandler<GetGroupTemplateListQuery, Result<PagedList<GroupTemplateDto>>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetGroupTemplateListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<GroupTemplateDto>>> Handle(GetGroupTemplateListQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.UserId,
                    cancellationToken);

            if (user is null)
                return Result<PagedList<GroupTemplateDto>>.Failed(
                    new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            var groupTemplateList = await GetPagedAsync(request.Page, request.Limit,
                _context.GroupTemplates.Where(x => x.IsDeleted == false && x.UserId == request.UserId),
                cancellationToken);

            return Result<PagedList<GroupTemplateDto>>.SuccessFul(groupTemplateList.MapTo<GroupTemplateDto>(_mapper));
        }
    }
}