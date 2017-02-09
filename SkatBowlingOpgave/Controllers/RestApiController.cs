using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SkatBowlingOpgave.Models;

namespace SkatBowlingOpgave.Controllers
{
    public class RestApiController : Controller
    {
        // GET: RestApi
        public async Task<ActionResult> Index()
        {
            RestApiViewModels restApiViewModels = new RestApiViewModels();
            var list = await restApiViewModels.Bowlingpoints();
            return View(list);
        }
    }
}