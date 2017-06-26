using PhotoStudio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.ViewModels;

namespace Website.Controllers
{
    [RoutePrefix("Store")]
    public class StoreController : Controller
    {
        private PhotoStudioContext db = new PhotoStudioContext();

        // GET: Store
        [Route("")]
        public ActionResult Index()
        {
            var vm = new StoreIndexViewModel();
            vm.Photos = db.GetAvailablePhotos(); 
            return View();
        }
    }
}