
 using System;
 using System.Data.Entity;
 using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using System.Collections.Generic;

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

        public DbSet<OrderData> _Orders { get; set; }
        public DbSet<PhotoData> _Photos { get; set; }

        public List<Entities.Photo> GetAvailablePhotos()
        {
            List<Entities.Photo> result = new List<Entities.Photo>();
            _Photos.ToList().ForEach(p => result.Add(new Entities.Photo(p.Name) { Id = p.Id }));
            return result; 
        }

        public int SubmitOrder(Entities.PrintOrder printOrder) 
        {
            OrderData order = new OrderData();
            foreach(var photo in printOrder.OrderItems)
            {
                var dbPhotoEntity = new PhotoData() { Name = photo.Name, Id = photo.Id };

                // This line is very important! 
                // This line tells the DbContext that the photo entity already exist in the DB and that it should 
                // be AWARE of it and track it, and set the state to unchanged (since I just add it to an order). 
                // Without this line, the SaveChanges will add thoes photos to the DB, with new ID's, since for
                // the DB context they are new and does not exist in the DB. 
                Entry(dbPhotoEntity).State = EntityState.Unchanged;
                order.Photos.Add(dbPhotoEntity); 
            }
            
            order.CustomerName = printOrder.CustomerName;
            _Orders.Add(order); // use AddRange for more than one order
            return SaveChanges(); 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderData>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderData>()
                .HasMany(e => e.Photos)
                .WithMany(e => e.Orders)
                .Map(m => m.ToTable("OrderLines").MapLeftKey("OrderId").MapRightKey("PhotoId"));

            modelBuilder.Entity<PhotoData>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<PhotoStudio.Entities.Photo> Photos { get; set; }
    }
}
