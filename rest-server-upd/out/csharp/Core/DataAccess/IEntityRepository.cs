using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    // T .. shows that T must be a class (like type document) vom typ IEntity (like parent type); new ().. it must be initializable

    public interface IEntityRepository<T> where T : class, IEntity, new() 
    {
        //inlude crud operations 
        List<T> GetAll(Expression<Func<T, bool>> filter = null); //optional filter
        T Get(Expression<Func<T, bool>> filter); 
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
