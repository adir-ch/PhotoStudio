using PhotoStudio.Data;
using pe = PhotoStudio.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class WebApiController : ApiController
    {
        // GET: api/WebApi
        public IEnumerable<pe.Photo> Get()
        {
            List<pe.Photo> photos;
            using (var db = new PhotoStudioContext())
            {
                db.Database.Log = Console.WriteLine;
                photos = db.GetAvailablePhotos();
            }

            //return new string[] { "value1", "value2" };
            return photos; 
        }

        // GET: api/WebApi/5
        public HttpResponseMessage Get(int id)
        {
            List<pe.Photo> photos;
            using (var db = new PhotoStudioContext())
            {
                db.Database.Log = Console.WriteLine;
                photos = db.GetAvailablePhotos();
            }

            var result = photos.Where(p => p.Id == id).FirstOrDefault();
            if (result == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Photo was not found");

            return Request.CreateResponse(result); 
        }

        [Route("api/WebApi/{id}/name")]
        public IHttpActionResult GetPhotoName(int id)
        {
            List<pe.Photo> photos;
            using (var db = new PhotoStudioContext())
            {
                db.Database.Log = Console.WriteLine;
                photos = db.GetAvailablePhotos();
            }

            var result = photos.Where(p => p.Id == id).FirstOrDefault();
            if (result == null)
                return NotFound(); 

            return Ok(result.Name);
        }


        // POST: api/WebApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WebApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebApi/5
        public void Delete(int id)
        {
        }
    }
}
