using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Transactions.Dto;
using DevTools.Common.Enum;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Transactions.Queries
{
    public class GetTransactionPagedListQuery : PagingOptions, IRequest<PagedList<TransactionDto>>
    {
        public TransactionStatus? TransactionStatus { get; set; }
    }

    public class GetTransactionPagedListQueryHandler : PagingService<Transaction>, IRequestHandler<GetTransactionPagedListQuery, PagedList<TransactionDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionPagedListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<TransactionDto>> Handle(GetTransactionPagedListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Transaction> transaction = _context.Transactions.Include(x => x.User);

            if (!string.IsNullOrWhiteSpace(request.Query))
                transaction = transaction.Where(x =>
                    x.RefId.Contains(request.Query)
                    || x.User.Name.Contains(request.Query) ||
                    x.User.Family.Contains(request.Query) ||
                    x.User.Email.Contains(request.Query));

            if (request.TransactionStatus.HasValue)
                transaction = transaction.Where(x => x.TransactionStatus == request.TransactionStatus);
            

            var transactionList = await GetPagedAsync(request.Page, request.Limit, transaction, cancellationToken);


            return transactionList.MapTo<TransactionDto>(_mapper);
        }
    }
}