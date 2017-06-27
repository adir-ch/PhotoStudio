
 using System;
 using System.Data.Entity;
 using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using System.Collections.Generic;
using PhotoStudio.Entities;

namespace PhotoStudio.Data
{
    public partial class PhotoStudioContext : DbContext
    {
        public PhotoStudioContext()
            : base("name=PhotoStudioContext")
        {
        }

        public PhotoStudioContext(string connection)
            : base(connection)
        {
        }

        public DbSet<PrintOrder> Orders { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public List<Photo> GetAvailablePhotos()
        {
            List<Photo> result = new List<Photo>();
            return Photos.ToList(); // .ForEach(p => result.Add(new Photo(p.Name) { Id = p.Id }));
            //return result; 
        }

        public List<Photo> GetAvailablePhotosByName(string photoName)
        {
            if (String.IsNullOrEmpty(photoName) == true || String.IsNullOrWhiteSpace(photoName) == true)
            {
                return GetAvailablePhotos(); 
            }
            return Photos.Where(p => p.Name.Contains(photoName)).ToList(); 
        }

        public int SubmitOrder(Entities.PrintOrder printOrder) 
        {
            PrintOrder order = new PrintOrder();
            foreach(var photo in printOrder.OrderItems)
            {
                var dbPhotoEntity = new Photo() { Name = photo.Name, Id = photo.Id };

                // This line is very important! 
                // This line tells the DbContext that the photo entity already exist in the DB and that it should 
                // be AWARE of it and track it, and set the state to unchanged (since I just add it to an order). 
                // Without this line, the SaveChanges will add thoes photos to the DB, with new ID's, since for
                // the DB context they are new and does not exist in the DB. 
                Entry(dbPhotoEntity).State = EntityState.Unchanged;
                order.OrderItems.Add(dbPhotoEntity); 
            }
            
            order.CustomerName = printOrder.CustomerName;
            Orders.Add(order); // use AddRange for more than one order
            return SaveChanges(); 
        }
    }
}
