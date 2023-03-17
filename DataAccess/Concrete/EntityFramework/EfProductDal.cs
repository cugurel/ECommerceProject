using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            using (Context c = new Context())
            {
                var addedEntity = c.Entry(entity);
                addedEntity.State = EntityState.Added;
                c.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (Context c = new Context())
            {
                var deletedEntity = c.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                c.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (Context c = new Context())
            {
                return c.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (Context c = new Context())
            {
                return filter == null
                ? c.Set<Product>().ToList() 
                : c.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (Context c = new Context())
            {
                var updatedEntity = c.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                c.SaveChanges();
            }
        }
    }
}
