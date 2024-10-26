namespace Sumodesk.Domain.Interfaces;

public interface IRepository<TEntity> : IReadOnlyRespository<TEntity>, IReadUpdateRepository<TEntity>
	where TEntity : class, IEntityRoot
{
	bool AutoSaveChanges { get; set; }
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
}
