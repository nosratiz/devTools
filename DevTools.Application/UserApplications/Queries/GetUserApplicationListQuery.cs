using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.UserApplications.Dto;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.UserApplications.Queries
{
    public class GetUserApplicationListQuery : IRequest<Result<List<UserApplicationDto>>>
    {
        public Guid UserId { get; set; }
    }


    public class GetUserApplicationListQueryHandler : IRequestHandler<GetUserApplicationListQuery, Result<List<UserApplicationDto>>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetUserApplicationListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<UserApplicationDto>>> Handle(GetUserApplicationListQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.UserApplications)
                .SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.UserId, cancellationToken);

            if (user is null)
                return Result<List<UserApplicationDto>>.Failed(
                    new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            return Result<List<UserApplicationDto>>.SuccessFul(_mapper.Map<List<UserApplicationDto>>(user.UserApplications));

        }
    }
}