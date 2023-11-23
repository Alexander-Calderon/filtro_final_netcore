using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain;
public interface IGenericRepository<T> where T : class
{
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    Task<T> GetById(object id);
    Task<IEnumerable<T>> GetAll ();
    IEnumerable<T> Find(Expression<Func<T,bool>> expression);


}
