using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Templates.Command.DeleteTemplate
{
    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, Result>
    {
        private readonly IDevToolsDbContext _context;

        public DeleteTemplateCommandHandler(IDevToolsDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (template is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TemplateNotFound)));

            _context.Templates.Remove(template);

            return Result.SuccessFul();

        }
    }
}