using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Settings.Model;
using DevTools.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Settings.Queries
{
    public class GetSettingQuery : IRequest<Result<SettingDto>>
    {
    }

    public class GetSettingQueryHandler : IRequestHandler<GetSettingQuery, Result<SettingDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetSettingQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SettingDto>> Handle(GetSettingQuery request, CancellationToken cancellationToken)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(cancellationToken);

            return Result<SettingDto>.SuccessFul(_mapper.Map<SettingDto>(setting));
        }
    }
}