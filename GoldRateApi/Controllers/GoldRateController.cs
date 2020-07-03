using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GoldRateApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoldRateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
