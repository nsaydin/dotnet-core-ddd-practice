using System;
using MediatR;

namespace Application.Queries
{
    public class GetTimeQuery : IRequest<DateTime>
    {
    }
}