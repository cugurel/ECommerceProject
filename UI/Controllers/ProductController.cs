using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Controllers
{
    public class ProductController : Controller
    {

        IProductService _productService;
        Context c = new Context();

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var result = _productService.GetAllWithCategoryName();
            return View(result.Data);
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

            return RedirectToAction("Index", "Product");
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
                _productService.Update(product);
            }
            else
            {
                _productService.Update(product);
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.GetById(id);
            _productService.Delete(result.Data);
            return RedirectToAction("Index", "Product");
        }
    }
}
