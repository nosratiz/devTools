using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Settings.Command
{
    public class UpdateSettingCommand : IRequest<Result>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string GoogleAnalytic { get; set; }
        public string GoogleMaster { get; set; }
        public string CopyRight { get; set; }
        public string Email { get; set; }
        public string SocialMedia { get; set; }

    }
}