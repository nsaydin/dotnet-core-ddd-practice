using System;
using System.Threading;
using System.Threading.Tasks;
using Core.WorkTime;
using MediatR;

namespace Application.Commands
{
    public class IncreaseTimeCommandHandler : IRequestHandler<IncreaseTimeCommand, DateTime>
    {
        private readonly IWorkTime _workTime;

        public IncreaseTimeCommandHandler(IWorkTime workTime)
        {
            _workTime = workTime;
        }

        public Task<DateTime> Handle(IncreaseTimeCommand command, CancellationToken cancellationToken)
        {
            _workTime.Increase(command.Hour);
            return Task.FromResult(_workTime.Now());
        }
    }
}