using Sumodesk.Domain.Abstractions;

namespace Sumodesk.Domain.Interfaces;

public interface IReadUpdateRepository<TEntity>
	where TEntity : class, IEntityRoot
{
	bool EnableTracking { get; set; }
	Task<TEntity?> GetByIdAsync(int id, IncludeProps<TEntity>? includeProps = default, CancellationToken cancellationToken = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
