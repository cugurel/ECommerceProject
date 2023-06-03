using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

ProductManager productManager = new ProductManager(new EfProductDal());

foreach (var item in productManager.GetAll().Data)
{
    Console.WriteLine(item.ProductName);
}
var result = productManager.GetAll().Message;
Console.WriteLine(result);