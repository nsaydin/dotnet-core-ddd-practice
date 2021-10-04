using System;

namespace Core.Domain.Entity
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}