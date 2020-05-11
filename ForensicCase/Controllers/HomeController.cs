using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForensicCase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ForensicCase.Repositories;

namespace ForensicCase.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index(string search = null, string order = null)
        {
            HomeViewModel viewModel = new HomeViewModel();
            ForensicRepository repo = new ForensicRepository(Configuration);
            if (String.IsNullOrEmpty(search) && String.IsNullOrEmpty(order))
            {
                viewModel.Flowers = new System.Data.DataTable();
                viewModel.Flowers = repo.GetFlowerList(null,"flower_name"); ;

                return View(viewModel);
            }
            var result = repo.GetFlowerList(search, order);

            viewModel.Flowers = new System.Data.DataTable();
            viewModel.Flowers = result;

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Reset()
        {
            return View();
        }
    }
}
