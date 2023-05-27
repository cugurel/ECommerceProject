using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

ProductManager productManager = new ProductManager(new EfProductDal());

foreach (var item in productManager.GetAll())
{
    Console.WriteLine(item.ProductName);
}
