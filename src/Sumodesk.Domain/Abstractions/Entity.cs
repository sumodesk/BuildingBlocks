using Sumodesk.Domain.Interfaces;

namespace Sumodesk.Domain.Abstractions;

public class Entity : IEntityId
{
    public int Id { get; set; }
}
