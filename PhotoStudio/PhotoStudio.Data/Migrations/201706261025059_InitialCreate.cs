namespace PhotoStudio.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrintOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PrintOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrintOrders", t => t.PrintOrder_Id)
                .Index(t => t.PrintOrder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "PrintOrder_Id", "dbo.PrintOrders");
            DropIndex("dbo.Photos", new[] { "PrintOrder_Id" });
            DropTable("dbo.Photos");
            DropTable("dbo.PrintOrders");
        }
    }
}
