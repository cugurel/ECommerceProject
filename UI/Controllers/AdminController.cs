using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        IProductService _productService;

        Context c = new Context();
        public AdminController(ILogger<AdminController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = c.Products.ToList();
            var colors = c.Colors.ToList();

            foreach (var product in products)
            {
                foreach (var color in colors)
                {
                    var anyMatch = c.ProductColorMatches.SingleOrDefault(x => x.ProductId == product.Id & x.ColorId == color.Id);
                    if (anyMatch != null)
                    {
                        continue;
                    }
                    ProductColorMatch match = new ProductColorMatch();
                    match.ProductId = product.Id;
                    match.ColorId = color.Id;
                    match.Status = true;
                    match.ImagePath = color.ImagePath;
                    match.ColorName = color.ColorName;
                    c.ProductColorMatches.Add(match);
                    c.SaveChanges();
                }
            }

            var result = _productService.GetAllWithCategoryName();
            return View(result.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}