﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Comrade.Domain.Bases;
using Comrade.Domain.Interfaces;

#endregion

namespace Comrade.Core.Helpers.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : IEntity
    {
        Task Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(int id);
        Task<TEntity?> GetById(int id);
        Task<TEntity?> GetById(int id, params string[] includes);
        Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>> projection);
        Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>>? projection, params string[]? includes);
        Task<TEntity> GetByValue(string value);
        Task<TEntity> GetByValue(string value, Expression<Func<TEntity, TEntity>>? projection);
        Task<bool> ValueExists(int id, string value);
        Task<bool> ChildrenExists(int id, Expression<Func<TEntity, bool>> predicate, string? include = null);
        Task<bool> IsUnique(Expression<Func<TEntity, bool>> predicate);
        IQueryable<LookupEntity> GetLookup();
        IQueryable<LookupEntity> GetLookup(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetLookupQuery(Expression<Func<TEntity, TEntity>> projection);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAsNoTracking();
        IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, TEntity>> projection);
        Task<bool> GetAllChildren(int id, Expression<Func<TEntity, bool>> predicate, string? include = null);
        Task<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);
    }
}