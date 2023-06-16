using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _productService.GetAllWithCategoryName();
            return View(result.Data.ToPagedList(page,10));
        }
    }
}