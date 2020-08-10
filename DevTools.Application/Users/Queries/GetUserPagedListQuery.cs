using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Users.Model;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Users.Queries
{
    public class GetUserPagedListQuery : PagingOptions, IRequest<Result<PagedList<UserDto>>>
    {

    }

    public class GetUserPagedListQueryHandler : PagingService<User>, IRequestHandler<GetUserPagedListQuery, Result<PagedList<UserDto>>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetUserPagedListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<UserDto>>> Handle(GetUserPagedListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> users = _context.Users
                .Include(x => x.Role)
                .Where(x => x.IsDeleted == false);


            if (!string.IsNullOrWhiteSpace(request.Query))
                users = users.Where(x => x.Name.Contains(request.Query)
                                         || x.Family.Contains(request.Query)
                                         || x.Email.Contains(request.Query)
                                         || x.Mobile.Contains(request.Query));


            var userList = await GetPagedAsync(request.Page, request.Limit, users, cancellationToken);

            return Result<PagedList<UserDto>>.SuccessFul(userList.MapTo<UserDto>(_mapper));

        }
    }
}