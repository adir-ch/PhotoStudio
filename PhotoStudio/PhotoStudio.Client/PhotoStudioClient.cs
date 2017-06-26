using PhotoStudio.Client.PhotoStudioWcfService;
using PhotoStudio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Client
{
    public class PhotoStudioClient
    {
        private List<Photo> _selectedPhotos;
        private PhotoStudioServiceClient _proxy = new PhotoStudioServiceClient(); 

        public PhotoStudioClient()
        {
            _selectedPhotos = new List<Photo>(); 
        }

        public List<Photo> SelectPhotosForPrintOrder()
        {
            return new List<Photo>() { new Photo() {  Id = 2, Name = "Photo2.jpg" },
                                       new Photo() {  Id = 4, Name = "Photo4.jpg" }};
        }   

        public void ShowPhotos()
        {
            var photos = _proxy.GetPhotos();
            foreach(var photo in photos)
            {
                Console.WriteLine("{0}", photo.Name);
            }
        }

        public void SubmitPrintOrder(string customerName, List<Photo> selectedPhotos)
        {
            PrintOrder order = new PrintOrder();
            order.CustomerName = customerName;
            order.OrderItems = selectedPhotos; 
            _proxy.SubmitPrintOrder(order); 
        }

        static void Main(string[] args)
        {
            PhotoStudioClient p = new PhotoStudioClient();
            Console.WriteLine("Available photos:");
            p.ShowPhotos();
            //Console.WriteLine("Write customer name");
            var customerName = "Adir-" + new Random().Next().ToString(); // Console.ReadLine();
            var orderedPhotos = p.SelectPhotosForPrintOrder();
            Console.WriteLine("Submitting order for customer: {0}", customerName);
            p.SubmitPrintOrder(customerName, orderedPhotos);
            Console.WriteLine("Order submitted");
            Console.ReadKey(); 
        }
    }
}
