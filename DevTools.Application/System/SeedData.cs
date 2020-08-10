using System;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.Helper;
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

            if (!await _context.Roles.AnyAsync(cancellationToken))
            {
                await _context.Roles.AddAsync(new Role
                {
                    Name = "Admin"
                }, cancellationToken);

                await _context.Roles.AddAsync(new Role
                {
                    Name = "Supporter"
                }, cancellationToken);

                await _context.Roles.AddAsync(new Role
                {
                    Name = "User"
                }, cancellationToken);

                await _context.SaveAsync(cancellationToken);
            }

            if (!await _context.Users.AnyAsync(cancellationToken))
            {
                await _context.Users.AddAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Nima",
                    Family = "Nosrati",
                    Email = "nimanosrati93@gmail.com",
                    RoleId = Role.Admin,
                    ConfirmCode = Guid.NewGuid().ToString("N"),
                    IsEmailConfirm = true,
                    IsMobileConfirm = true,
                    Mobile = "989107602786",
                    RegisterDate = DateTime.Now,
                    ExpiredDate = DateTime.Now.AddDays(2),
                    Wallet = 10000,
                    Password = Utils.HashPass("nima1372@")
                }, cancellationToken);

                await _context.Users.AddAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Mohsen",
                    Family = "Kermanifar",
                    Email = "mohsen.kermanifar@yahoo.com",
                    RoleId = Role.Admin,
                    ConfirmCode = Guid.NewGuid().ToString("N"),
                    IsEmailConfirm = true,
                    IsMobileConfirm = true,
                    Mobile = "989190044601",
                    RegisterDate = DateTime.Now,
                    ExpiredDate = DateTime.Now.AddDays(2),
                    Wallet = 10000,
                    Password = Utils.HashPass("KermanifarAzOunast@")
                }, cancellationToken);

                await _context.SaveAsync(cancellationToken);
            }

            if (!await _context.Settings.AnyAsync(cancellationToken))
            {
                await _context.Settings.AddAsync(new Setting
                {
                    CopyRight = "copy Right by Dev Tools",
                    Title = "DevTools",
                    Description = "this site to help you to more concentrate on your code",
                    Email = "info@devtools.com",
                    Logo = ""
                }, cancellationToken);

                await _context.SaveAsync(cancellationToken);
            }
        }
    }
}