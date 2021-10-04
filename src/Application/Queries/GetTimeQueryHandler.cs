using System;
using System.Threading;
using System.Threading.Tasks;
using Core.WorkTime;
using MediatR;

namespace Application.Queries
{
    public class GetTimeQueryHandler : IRequestHandler<GetTimeQuery, DateTime>
    {
        private readonly IWorkTime _workTime;

        public GetTimeQueryHandler(IWorkTime workTime)
        {
            _workTime = workTime;
        }

        public Task<DateTime> Handle(GetTimeQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(_workTime.Now());
        }
    }
}