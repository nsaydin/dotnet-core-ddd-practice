using System;
using MediatR;

namespace Application.Commands
{
    public class IncreaseTimeCommand : IRequest<DateTime>
    {
        public int Hour { get; set; }
    }
}