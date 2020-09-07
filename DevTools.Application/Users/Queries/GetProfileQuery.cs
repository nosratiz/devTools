using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Users.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotImplementedException = System.NotImplementedException;

namespace DevTools.Application.Users.Queries
{
    public  class GetProfileQuery : IRequest<UserDto>
    {
    }

    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserDto>
    {
        private readonly IDevToolsDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(IDevToolsDbContext context, ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(
                x => x.IsDeleted == false && x.Id == Guid.Parse(_currentUserService.UserId), cancellationToken);
            
            return _mapper.Map<UserDto>(user);
        }
    }
}