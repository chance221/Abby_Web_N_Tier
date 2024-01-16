using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Abby.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDBContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDBContext db)
        {
            _db = db;
            dbSet=db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }


            //Takes the include properties if you want something returned from the database
            //not in the original request. Sirst it strips out blanks. It then adds the
            //name of the property to be included when the query is returned from the 
            //database. Thes way whenever you are retrieving records the ID along with
            //the include properties will be returned. If we did not do this some properties 
            //we will want from the DB will not be available 
            if (includeProperties != null)
            {
                foreach(var property in includeProperties.Split(
                    
                    new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
                
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries
                ))
                {
                    query = query.Include(property);
                }
            }                

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
