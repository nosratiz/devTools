using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Users.Model;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;

namespace DevTools.Application.Users.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            await _context.Users.AddAsync(user, cancellationToken);

            await _context.SaveAsync(cancellationToken);

            return Result<UserDto>.SuccessFul(_mapper.Map<UserDto>(user));

        }
    }
}