using Microsoft.EntityFrameworkCore;
using Sumodesk.Domain;
using System.Linq.Expressions;

namespace Sumodesk.Infrastructure.Data.Repositories;

public abstract class BaseRepository<TEntity>
	where TEntity : class
{
	protected BaseRepository(DomainDbContext dbContext)
	{
		DbContext = dbContext;
		DbSet = DbContext.Set<TEntity>();
	}

	public DomainDbContext DbContext { get; }
	public DbSet<TEntity> DbSet { get; }
	public bool EnableTracking { get; set; } = false;

	public bool AutoSaveChanges { get; set; } = true;

	protected async Task SaveChangesAsync(CancellationToken cancellationToken)
	{
		try
		{
			await DbContext.SaveChangesAsync(cancellationToken);
		}
		catch (Exception ex)
		{

			throw new RepositoryException("An exception occurred in the database while trying to save the data.", ex);
		}
	}

	protected IQueryable<TEntity> IncludeProps(IQueryable<TEntity> queryable, IEnumerable<Expression<Func<TEntity, object>>> includes)
	{
		var query = queryable;

		foreach (var include in includes)
		{
			query = query.Include(include);
		}

		return query;
	}
}

