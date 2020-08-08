using System;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.System
{
    public class SeedData
    {
        private readonly IDevToolsDbContext _context;

        public SeedData(IDevToolsDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!await _context.Users.AnyAsync(cancellationToken))
            {
               await _context.Users.AddAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Name="Nima",
                    Family="Nosrati",
                    Email="nimanosrati93@gmail.com",
                    

                });

            }
        }
    }
}