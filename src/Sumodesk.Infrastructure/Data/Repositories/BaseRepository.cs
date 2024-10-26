using Microsoft.EntityFrameworkCore;
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

