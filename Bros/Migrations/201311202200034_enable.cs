namespace Bros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Owner_ID = c.Int(),
                        Title = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .Index(t => t.ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.BroRequest",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Sender_ID = c.Int(),
                        Receiver_ID = c.Int(),
                        User_ID = c.Int(),
                        User_ID1 = c.Int(),
                        Message = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Sender_ID)
                .ForeignKey("dbo.User", t => t.Receiver_ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .ForeignKey("dbo.User", t => t.User_ID1)
                .Index(t => t.ID)
                .Index(t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.User_ID)
                .Index(t => t.User_ID1);
            
            CreateTable(
                "dbo.Circle",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Owner_ID = c.Int(),
                        User_ID = c.Int(),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .Index(t => t.ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Owner_ID = c.Int(),
                        ParentPost_ID = c.Int(),
                        Content = c.String(unicode: false),
                        IsFlagged = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .ForeignKey("dbo.Post", t => t.ParentPost_ID)
                .Index(t => t.ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.ParentPost_ID);
            
            CreateTable(
                "dbo.Hobby",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Profile_ID = c.Int(),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .Index(t => t.ID)
                .Index(t => t.Profile_ID);
            
            CreateTable(
                "dbo.Liking",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Profile_ID = c.Int(),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .Index(t => t.ID)
                .Index(t => t.Profile_ID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Sender_ID = c.Int(),
                        Receiver_ID = c.Int(),
                        User_ID = c.Int(),
                        Content = c.String(unicode: false),
                        DateSeen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Sender_ID)
                .ForeignKey("dbo.User", t => t.Receiver_ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .Index(t => t.ID)
                .Index(t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Receiver_ID = c.Int(),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Receiver_ID)
                .Index(t => t.ID)
                .Index(t => t.Receiver_ID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Owner_ID = c.Int(),
                        IsFlagged = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .Index(t => t.ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Album_ID = c.Int(),
                        Caption = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.ID)
                .ForeignKey("dbo.Album", t => t.Album_ID)
                .Index(t => t.ID)
                .Index(t => t.Album_ID);
            
            CreateTable(
                "dbo.Preference",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Owner_ID = c.Int(),
                        Key = c.String(unicode: false),
                        Value = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .Index(t => t.ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageFile = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Catagory = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        ZipCode = c.String(unicode: false),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.TextPost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Content = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Profile_ID = c.Int(nullable: false),
                        Circle_ID = c.Int(),
                        Email = c.String(unicode: false),
                        password = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.ID)
                .ForeignKey("dbo.Profile", t => t.Profile_ID)
                .ForeignKey("dbo.Circle", t => t.Circle_ID)
                .Index(t => t.ID)
                .Index(t => t.Profile_ID)
                .Index(t => t.Circle_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Circle_ID" });
            DropIndex("dbo.User", new[] { "Profile_ID" });
            DropIndex("dbo.User", new[] { "ID" });
            DropIndex("dbo.TextPost", new[] { "ID" });
            DropIndex("dbo.Profile", new[] { "ID" });
            DropIndex("dbo.Product", new[] { "ID" });
            DropIndex("dbo.Preference", new[] { "Owner_ID" });
            DropIndex("dbo.Preference", new[] { "ID" });
            DropIndex("dbo.Photo", new[] { "Album_ID" });
            DropIndex("dbo.Photo", new[] { "ID" });
            DropIndex("dbo.Post", new[] { "Owner_ID" });
            DropIndex("dbo.Post", new[] { "ID" });
            DropIndex("dbo.Notification", new[] { "Receiver_ID" });
            DropIndex("dbo.Notification", new[] { "ID" });
            DropIndex("dbo.Message", new[] { "User_ID" });
            DropIndex("dbo.Message", new[] { "Receiver_ID" });
            DropIndex("dbo.Message", new[] { "Sender_ID" });
            DropIndex("dbo.Message", new[] { "ID" });
            DropIndex("dbo.Liking", new[] { "Profile_ID" });
            DropIndex("dbo.Liking", new[] { "ID" });
            DropIndex("dbo.Hobby", new[] { "Profile_ID" });
            DropIndex("dbo.Hobby", new[] { "ID" });
            DropIndex("dbo.Comment", new[] { "ParentPost_ID" });
            DropIndex("dbo.Comment", new[] { "Owner_ID" });
            DropIndex("dbo.Comment", new[] { "ID" });
            DropIndex("dbo.Circle", new[] { "User_ID" });
            DropIndex("dbo.Circle", new[] { "Owner_ID" });
            DropIndex("dbo.Circle", new[] { "ID" });
            DropIndex("dbo.BroRequest", new[] { "User_ID1" });
            DropIndex("dbo.BroRequest", new[] { "User_ID" });
            DropIndex("dbo.BroRequest", new[] { "Receiver_ID" });
            DropIndex("dbo.BroRequest", new[] { "Sender_ID" });
            DropIndex("dbo.BroRequest", new[] { "ID" });
            DropIndex("dbo.Album", new[] { "Owner_ID" });
            DropIndex("dbo.Album", new[] { "ID" });
            DropForeignKey("dbo.User", "Circle_ID", "dbo.Circle");
            DropForeignKey("dbo.User", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.User", "ID", "dbo.Entities");
            DropForeignKey("dbo.TextPost", "ID", "dbo.Post");
            DropForeignKey("dbo.Profile", "ID", "dbo.Entities");
            DropForeignKey("dbo.Product", "ID", "dbo.Entities");
            DropForeignKey("dbo.Preference", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.Preference", "ID", "dbo.Entities");
            DropForeignKey("dbo.Photo", "Album_ID", "dbo.Album");
            DropForeignKey("dbo.Photo", "ID", "dbo.Post");
            DropForeignKey("dbo.Post", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.Post", "ID", "dbo.Entities");
            DropForeignKey("dbo.Notification", "Receiver_ID", "dbo.User");
            DropForeignKey("dbo.Notification", "ID", "dbo.Entities");
            DropForeignKey("dbo.Message", "User_ID", "dbo.User");
            DropForeignKey("dbo.Message", "Receiver_ID", "dbo.User");
            DropForeignKey("dbo.Message", "Sender_ID", "dbo.User");
            DropForeignKey("dbo.Message", "ID", "dbo.Entities");
            DropForeignKey("dbo.Liking", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.Liking", "ID", "dbo.Entities");
            DropForeignKey("dbo.Hobby", "Profile_ID", "dbo.Profile");
            DropForeignKey("dbo.Hobby", "ID", "dbo.Entities");
            DropForeignKey("dbo.Comment", "ParentPost_ID", "dbo.Post");
            DropForeignKey("dbo.Comment", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.Comment", "ID", "dbo.Entities");
            DropForeignKey("dbo.Circle", "User_ID", "dbo.User");
            DropForeignKey("dbo.Circle", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.Circle", "ID", "dbo.Entities");
            DropForeignKey("dbo.BroRequest", "User_ID1", "dbo.User");
            DropForeignKey("dbo.BroRequest", "User_ID", "dbo.User");
            DropForeignKey("dbo.BroRequest", "Receiver_ID", "dbo.User");
            DropForeignKey("dbo.BroRequest", "Sender_ID", "dbo.User");
            DropForeignKey("dbo.BroRequest", "ID", "dbo.Entities");
            DropForeignKey("dbo.Album", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.Album", "ID", "dbo.Entities");
            DropTable("dbo.User");
            DropTable("dbo.TextPost");
            DropTable("dbo.Profile");
            DropTable("dbo.Product");
            DropTable("dbo.Preference");
            DropTable("dbo.Photo");
            DropTable("dbo.Post");
            DropTable("dbo.Notification");
            DropTable("dbo.Message");
            DropTable("dbo.Liking");
            DropTable("dbo.Hobby");
            DropTable("dbo.Comment");
            DropTable("dbo.Circle");
            DropTable("dbo.BroRequest");
            DropTable("dbo.Album");
            DropTable("dbo.Entities");
        }
    }
}
