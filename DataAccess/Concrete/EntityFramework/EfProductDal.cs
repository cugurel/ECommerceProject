using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product,Context>, IProductDal
    {
        public List<ProductWithCategory> GetProductWithCategoryName()
        {
            using (Context c = new Context())
            {
                var result = from p in c.Products
                             join ct in c.Categories
                             on p.CategoryId equals ct.Id
                             select new ProductWithCategory
                             {
                                 ProductId = p.Id,
                                 CategoryName = ct.CategoryName,
                                 UnitPrice = (decimal)p.UnitPrice,
                                 UnitsInStock = (int)p.UnitsInStock,
                                 ProductName = p.ProductName,
                                 ConsumeMethod = p.ConsumeMethod
                             };

                return result.ToList();
            }
        }

        public ProductDetailDto GetProuctDetail(int id)
        {
            using (Context c = new Context())
            {
                var result = from p in c.Products
                             join ct in c.Categories
                             on p.CategoryId equals ct.Id
                             select new ProductDetailDto
                             {
                                 ProductId = p.Id,
                                 CategoryName = ct.CategoryName,
                                 UnitPrice = (decimal)p.UnitPrice,
                                 UnitsInStock = (int)p.UnitsInStock,
                                 ProductName = p.ProductName,
                                 ConsumeMethod = p.ConsumeMethod,
                                 ImagePath = p.ImagePath,
                                 Consume = p.Consume
                             };

                return result.SingleOrDefault(x=>x.ProductId == id);
            }
        }
    }
}
