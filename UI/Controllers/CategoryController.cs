using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result = _categoryService.GetAll();
            return View(result.Data);
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            var result = _categoryService.GetAll();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index","Category");
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
            _categoryService.Update(category);
            return RedirectToAction("Index", "Category");
        }

        public IActionResult DeleteCategory(int id)
        {
            var result = _categoryService.GetById(id);
            _categoryService.Delete(result.Data);
            return RedirectToAction("Index", "Category");
        }
    }
}
