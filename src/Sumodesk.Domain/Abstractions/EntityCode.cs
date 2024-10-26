using Sumodesk.Domain.Interfaces;

namespace Sumodesk.Domain.Abstractions;

public class EntityCode : Entity, IEntityCode
{
    public required string Code { get; set; }
}
