using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ChurchWebApp_DAL.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> getRecords();
        void delete(TEntity entity);
        void deleteBasedOnConditions(Func<TEntity, bool> where);
        bool exits(object primarykey);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> get();
        IEnumerable<TEntity> getAllRecords();
        TEntity getById(int? id);
        TEntity getFirst(Func<TEntity, bool> predicate);
        IQueryable<TEntity> getManyWithQuarable(Func<TEntity, bool> where);
        IEnumerable<TEntity> getManyWithwhere(Func<TEntity, bool> Where);
        TEntity getSingle(Func<TEntity, bool> predicate);
        TEntity getWithFirstOrDefault(Func<TEntity, bool> where);
        IQueryable<TEntity> getWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include);
        void insert(TEntity entity);
        TEntity getWithFirstInDecendingOrder(Func<TEntity, Boolean> where);
        void Update(TEntity entityToUpdate);

        IQueryable<TEntity> Include(params string[] include);
        void CommitChanges();
    }
}