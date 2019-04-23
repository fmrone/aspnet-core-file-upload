using FMR.Image.Business;
using FMR.Image.Mvc.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FMR.Image.Mvc.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageBusiness _imageBusiness;

        public ImageController(ImageBusiness imageBusiness)
        {
            _imageBusiness = imageBusiness;
        }

        public IActionResult Index()
        {
            var model = new ImageViewModel();

            model.Images = _imageBusiness.FindAll().ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(IFormFile file, ImageViewModel model)
        {
            if (file == null || file.Length == 0)
                ModelState.AddModelError("", "Nenhuma imagem foi postada");

            if (!ModelState.IsValid)
                return await Task.Run(() => View("Index", model));

            byte[] array = null;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    array = ms.GetBuffer();
                }
            }
            catch
            {
                ModelState.AddModelError("", "Falha no processamento da imagem");
            }

            if (!ModelState.IsValid)
                return await Task.Run(() => View("Index", model));

            var imageEntity = new Data.Entities.Image
            {
                File = array,
                FileExtention = Path.GetExtension(file.FileName),
                Name = model.Name,
                Description = model.Description
            };

            var commandResult = _imageBusiness.Create(imageEntity);

            if (!commandResult.Success)
            {
                foreach (var notification in commandResult.Notifications)
                {
                    ModelState.AddModelError("", notification);
                }

                return await Task.Run(() => View("Index", model));
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var commandResult = await Task.Run(() => _imageBusiness.Delete(id));

            return RedirectToAction("Index");
        }
    }
}