using PhotoStudio.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Services
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)] // New service for every request 
    public class PhotoStudioService : IPhotoStudioService, IDisposable
    {
        public PhotoStudioService()
        {
            // this line prevents from DB initialize data to appear in the console log, needed in prod
            Database.SetInitializer(new NullDatabaseInitializer<PhotoStudioContext>()); 
        }

        public List<Entities.Photo> GetPhotos()
        {
            Console.WriteLine("Request: Get Photos");

            List<Entities.Photo> photos; 
            using(var db = new PhotoStudioContext())
            {
                db.Database.Log = Console.WriteLine; 
                photos = db.GetAvailablePhotos();
            }
            
            return photos;
        }

        [OperationBehavior(TransactionScopeRequired=true)] // roll back if there is a problem
        public void SubmitPrintOrder(Entities.PrintOrder order)
        {
            using (var db = new PhotoStudioContext())
            {
                db.Database.Log = Console.WriteLine; 
                db.SubmitOrder(order);
                Console.WriteLine("Request: Order {0} for client {1} received, printing {2} photos", order.OrderId, order.CustomerName, order.OrderItems.Count());
            }
        }

        public void Dispose()
        {
            // do nothing for now
        }
    }
}
