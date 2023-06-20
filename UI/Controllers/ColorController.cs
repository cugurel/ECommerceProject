using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ColorController : Controller
    {
        IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        public IActionResult Index()
        {
            var result = _colorService.GetAll();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddColor()
        {
            var result = _colorService.GetAll();
            return View(result.Data);
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

            return RedirectToAction("Index", "Color");
        }

        [HttpGet]
        public IActionResult UpdateColor(int id)
        {
            var result = _colorService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateColor(Color color)
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
                _colorService.Update(color);
            }
            else
            {
                _colorService.Update(color);
            }

            return RedirectToAction("Index", "Color");
        }

        public IActionResult DeleteColor(int id)
        {
            var result = _colorService.GetById(id);
            _colorService.Delete(result.Data);
            return RedirectToAction("Index","Color");
        }
    }
}
