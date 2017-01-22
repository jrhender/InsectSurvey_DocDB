using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using InsectSurvey.Models;

namespace InsectSurvey.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbSettings _dbSettings;

        public HomeController(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("About")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await DocumentDBRepository<Item>.GetItemsAsync(d => !d.Completed, _dbSettings.DatabaseID, _dbSettings.Collection);
            return View(items);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
