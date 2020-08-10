using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Users.Model;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Users.Queries
{
    public class GetUserQuery : IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id, cancellationToken);

            if (user is null)
                return Result<UserDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));


            return Result<UserDto>.SuccessFul(_mapper.Map<UserDto>(user));
        }
    }
}