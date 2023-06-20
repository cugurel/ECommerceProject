using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Presentation.Controllers
{
    public class ColorsController : Controller
    {
        IColorService _colorService;
        Context c = new Context();
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ColorsController(IColorService colorService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _colorService = colorService;
            _environment = environment;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _colorService.GetAll();
            return View(result.Data.ToPagedList(page, 10));
        }

        public IActionResult DeleteColor(int id)
        {
            var value = _colorService.GetById(id);
            _colorService.Delete(value.Data);
            return RedirectToAction("Index", "Colors");
        }

        [HttpGet]
        public IActionResult AddColor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddColor(Color color)
        {
            if (color.File != null)
            {
                var item = color.File;
                var extent = Path.GetExtension(item.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\ColorImages", randomName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                color.ImagePath = randomName;
                _colorService.Add(color);
            }
            else
            {
                _colorService.Add(color);
            }

            return RedirectToAction("Index", "Colors");
        }

        [HttpGet]
        public IActionResult UpdateColor(int id)
        {
            var result = _colorService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateColor(Color color)
        {
            _colorService.Update(color);
            return RedirectToAction("Index", "Colors");
        }
    }
}
