using Sumodesk.Domain.Abstractions;

namespace Sumodesk.Domain.Interfaces;

public interface IReadOnlyRespository<TEntity>
	where TEntity : class
{
	Task<TEntity?> GetOneAsync(QueryFilter<TEntity> filter, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<TEntity>> GetAllAsync(IncludeProps<TEntity>? includeProps = default, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<TEntity>> GetFilteredAsync(QueryFilter<TEntity> filter, CancellationToken cancellationToken = default);
	Task<int> CountAsync(QueryFilter<TEntity>? filter = default, CancellationToken cancellationToken = default);
	Task<bool> AnyAsync(QueryFilter<TEntity>? filter = default, CancellationToken cancellationToken = default);
}
