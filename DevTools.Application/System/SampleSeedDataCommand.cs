using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using MediatR;

namespace DevTools.Application.System
{
    public class SampleSeedDataCommand : IRequest
    {
    }

    public class SampleSeedDataCommandHandler : IRequestHandler<SampleSeedDataCommand>
    {
        private readonly IDevToolsDbContext _context;

        public SampleSeedDataCommandHandler(IDevToolsDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SampleSeedDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SeedData(_context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}