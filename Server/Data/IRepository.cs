using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Server.Data
{
	public interface IRepository<TEntity> where TEntity : class
    {
	    TEntity Get(Guid id);
	    IEnumerable<TEntity> GetAll();
	    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
	    void Add(TEntity entity);

	    void Update(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);
	    void Remove(TEntity entity);
	    void RemoveRange(IEnumerable<TEntity> entities);
	}
}
