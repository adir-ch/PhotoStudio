using PhotoStudio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.ViewModels;
using Website.Models;
using Newtonsoft.Json;

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
            var data = db.GetAvailablePhotos();
            data.ForEach(item => vm.Photos.Add(new Photo() { Id = item.Id, Name = item.Name }));
            return View(vm);
        }

        //// GET: Store/query
        //[Route("Search/{query ?}")]
        //public JsonResult Search(StoreIndexViewModel viewModel)
        //{

        //    var data = db.GetAvailablePhotosByName(viewModel.SearchQuery); 
        //    List<Photo> searchResults = new List<Photo>(); 
        //    data.ForEach(item => searchResults.Add(new Photo() { Name = item.Name }));
        //    var json = JsonConvert.SerializeObject(searchResults);
        //    return Json(json, JsonRequestBehavior.AllowGet); 
        //}

        // GET: Store/query
        [Route("Search/{query ?}")]
        public PartialViewResult Search(StoreIndexViewModel viewModel)
        {

            var data = db.GetAvailablePhotosByName(viewModel.SearchQuery);
            List<Photo> searchResults = new List<Photo>();
            data.ForEach(item => searchResults.Add(new Photo() { Id = item.Id, Name = item.Name }));
            return PartialView("_SearchResults", searchResults);
        }
    }
}