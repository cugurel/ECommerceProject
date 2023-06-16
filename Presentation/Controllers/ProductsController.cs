using Business.Abstract;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;
        Context c = new Context();
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProductsController(IProductService productService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _productService = productService;
            _environment = environment;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _productService.GetAllWithCategoryName();
            return View(result.Data.ToPagedList(page , 10));
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.GetById(id);
            _productService.Delete(value.Data);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            IEnumerable<SelectListItem> categoryValues = (from x in c.Categories
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.Id.ToString(),
                                                   }).ToList();

            ViewBag.CategoryList = categoryValues;

            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            product.Id = 0;
            IEnumerable<SelectListItem> categoryValues = (from x in c.Categories
                                                          select new SelectListItem
                                                          {
                                                              Text = x.CategoryName,
                                                              Value = x.Id.ToString(),
                                                          }).ToList();

            ViewBag.CategoryList = categoryValues;

            if (product.File != null)
            {
                var item = product.File;
                var extent = Path.GetExtension(item.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\ProductImages", randomName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                product.ImagePath = randomName;
                _productService.Add(product);
            }
            else
            {
                _productService.Add(product);
            }
            
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            IEnumerable<SelectListItem> categoryValues = (from x in c.Categories
                                                          select new SelectListItem
                                                          {
                                                              Text = x.CategoryName,
                                                              Value = x.Id.ToString(),
                                                          }).ToList();

            ViewBag.CategoryList = categoryValues;

            var result = _productService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            IEnumerable<SelectListItem> categoryValues = (from x in c.Categories
                                                          select new SelectListItem
                                                          {
                                                              Text = x.CategoryName,
                                                              Value = x.Id.ToString(),
                                                          }).ToList();

            ViewBag.CategoryList = categoryValues;

            _productService.Update(product);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var result = _productService.GetProductDetail(id);
            return View(result.Data);
        }
    }
}
