using System.Threading.Tasks;
using ax.controlPanel.Models;
using ax.fileProcessor;
using ax.fileProcessor.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ax.controlPanel.Controllers
{
    public class DashboardController : Controller
    {
        public IZipFileProcessor ZipFileProcessor { get; private set; }
        public IZipContentStorageHelper ZipContentStorageHelper { get; set; }
        public ILogger<DashboardController> Logger { get; set; }

        public DashboardController(IZipFileProcessor zipFileProcessor, IZipContentStorageHelper zipContentStorageHelper, ILogger<DashboardController> logger)
        {
            ZipFileProcessor = zipFileProcessor;
            ZipContentStorageHelper = zipContentStorageHelper;
            Logger = logger;
        }


        // GET: /<controller>/
        public IActionResult Upload()
        {
            return View(new UploadZipFileModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadZipFileModel uploadZipFileModel)
        {
            if (ModelState.IsValid)
            {
                var result = ZipFileProcessor.Process(uploadZipFileModel.ZipFile);

                if (result.IsFailure)
                {
                    Logger.LogError(result.Error);
                    ModelState.AddModelError("ZipFile", result.Error);
                    return View(uploadZipFileModel);
                }

                var auth = new AuthCredential
                {
                    Password = uploadZipFileModel.Password,
                    Username = uploadZipFileModel.Username
                };

                var responseMessage = await ZipContentStorageHelper.SendContent(result.Value, auth);

                if (responseMessage.IsFailure)
                {
                    Logger.LogError(responseMessage.Error, result.Value);
                    ModelState.AddModelError("ZipFile", responseMessage.Error);
                    return View(uploadZipFileModel);
                }

                return RedirectToAction("Success");
            }

            return View(uploadZipFileModel);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
