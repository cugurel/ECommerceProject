using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _categoryService.GetAll();
            return View(result.Data.ToPagedList(page,10));
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index", "Categories");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var result = _categoryService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index", "Categories");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.GetById(id);
            _categoryService.Delete(value.Data);
            return RedirectToAction("Index", "Categories");
        }
    }
}
