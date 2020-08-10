using System.Threading.Tasks;
using DevTools.Application.Settings.Command;
using DevTools.Application.Settings.Model;
using DevTools.Application.Settings.Queries;
using DevTools.Common.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Admin
{
    [Authorize(Roles = "Admin,Supporter")]
    public class SettingsController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// setting Site
        /// </summary>
        /// <returns>Setting</returns>
        /// <response code="200">if Setting exist </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(SettingDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> GetSetting()
        {
            var result = await _mediator.Send(new GetSettingQuery());

            return result.ApiResult;
        }

        /// <summary>
        /// update setting Site
        /// </summary>
        /// <returns>update Setting</returns>
        /// <response code="200">if update Setting successfully </response>
        /// <response code="400">if Validation Failed  </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> UpdateSetting(UpdateSettingCommand updateSettingCommand)
        {
            var result = await _mediator.Send(updateSettingCommand);

            return result.ApiResult;
        }
    }
}
