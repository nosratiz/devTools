using System;
using System.Threading.Tasks;
using DevTools.Application.Settings.Model;
using DevTools.Application.Templates.Command.DeleteTemplate;
using DevTools.Application.Templates.Dto;
using DevTools.Application.Templates.Queries;
using DevTools.Common.General;
using DevTools.Common.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Admin
{
    public class TemplatesController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public TemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// template List
        /// </summary>
        /// <response code="200">get list of template </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<TemplateListDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> GetTemplates([FromQuery] GetTemplatePagedListQuery query)
            => Ok(await _mediator.Send(query));



        /// <summary>
        /// Get Template
        /// </summary>
        /// <response code="200">get Template back</response>
        ///  <response code="404">if template not found </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TemplateListDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetTemplateQuery { Id = id });

            return result.ApiResult;
        }


        /// <summary>
        /// Get Template
        /// </summary>
        /// <response code="204">get Template back</response>
        ///  <response code="404">if template not found </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TemplateListDto), 204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTemplateCommand { Id = id });

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();

        }

    }
}
