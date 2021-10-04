using System;

namespace Core.Domain.Entity
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}