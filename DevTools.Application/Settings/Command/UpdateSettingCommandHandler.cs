using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Settings.Command
{
    public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand, Result>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSettingCommandHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(cancellationToken);

            _mapper.Map(request, setting);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.UpdateSettingSuccessfully)));
        }
    }
}