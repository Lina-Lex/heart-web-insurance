using System;

namespace Domain.Interfaces.Domain
{
    public interface IDbBaseEntity
    {
        Guid Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime? LastUpdatedDate { get; set; }
    }
}
