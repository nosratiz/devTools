using System;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using Newtonsoft.Json;

namespace DevTools.Application.System
{
    public class SeedData
    {
        private readonly IDevToolsDbContext _context;

        public SeedData(IDevToolsDbContext Context)
        {
            _context = Context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!await _context.Notifiers.AnyAsync(cancellationToken))
            {
                await _context.Notifiers.AddAsync(new Notifier
                {
                    Name = "Nosrati",
                    CreateDate = DateTime.Now,
                    ServiceType = ServiceType.Smtp,
                    Setting = JsonConvert.SerializeObject(new EmailSetting
                    {
                        From = "nimanosrati93@hotmail.com",
                        Port = 587,
                        UserName = "nimanosrati93@hotmail.com",
                        Password = "0016057015nosrati@",
                        Host = "smtp.live.com",
                        EnableSsl = true
                    })
                }, cancellationToken);

                await _context.SaveAsync(cancellationToken);
            }
        }
    }
}