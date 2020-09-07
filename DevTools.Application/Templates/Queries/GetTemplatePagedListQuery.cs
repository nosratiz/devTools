using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Templates.Dto;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Templates.Queries
{
    public class GetTemplatePagedListQuery : PagingOptions, IRequest<PagedList<TemplateListDto>>
    {

    }

    public class GetTemplatePagedListQueryHandler : PagingService<Template>, IRequestHandler<GetTemplatePagedListQuery, PagedList<TemplateListDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetTemplatePagedListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<TemplateListDto>> Handle(GetTemplatePagedListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Template> templates = _context.Templates
                .Include(x => x.GroupTemplate)
                .ThenInclude(x => x.User);

            if (!string.IsNullOrWhiteSpace(request.Query))
                templates = templates.Where(x => x.Name.Contains(request.Query)
                                                 || x.GroupTemplate.Name.Contains(request.Query)
                                                 || x.GroupTemplate.User.Name.Contains(request.Query)
                                                 || x.GroupTemplate.User.Family.Contains(request.Query));

            var templateLIst = await GetPagedAsync(request.Page, request.Limit, templates, cancellationToken);

            return templateLIst.MapTo<TemplateListDto>(_mapper);
        }
    }
}