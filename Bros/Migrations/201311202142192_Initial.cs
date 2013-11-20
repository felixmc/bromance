namespace Bros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Email = c.String(unicode: false),
                        password = c.Binary(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Circle_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.ID)
                .ForeignKey("dbo.Circles", t => t.Circle_ID)
                .Index(t => t.ID)
                .Index(t => t.Circle_ID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Gender = c.Int(nullable: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        ZipCode = c.String(unicode: false),
                        BirthDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Hobbies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Profile_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.Profile_ID)
                .Index(t => t.Profile_ID);
            
            CreateTable(
                "dbo.Likings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Profile_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.Profile_ID)
                .Index(t => t.Profile_ID);
            
            CreateTable(
                "dbo.Circles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Owner_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.BroRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Sender_ID = c.Int(),
                        Receiver_ID = c.Int(),
                        User_ID = c.Int(),
                        User_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Sender_ID)
                .ForeignKey("dbo.Users", t => t.Receiver_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Users", t => t.User_ID1)
                .Index(t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.User_ID)
                .Index(t => t.User_ID1);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsRead = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Receiver_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Receiver_ID)
                .Index(t => t.Receiver_ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(unicode: false),
                        DateSeen = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Sender_ID = c.Int(),
                        Receiver_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Sender_ID)
                .ForeignKey("dbo.Users", t => t.Receiver_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsFlagged = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Caption = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Owner_ID = c.Int(),
                        Album_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .ForeignKey("dbo.Albums", t => t.Album_ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.Album_ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(unicode: false),
                        IsFlagged = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Owner_ID = c.Int(),
                        ParentPost_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .ForeignKey("dbo.Posts", t => t.ParentPost_ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.ParentPost_ID);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Owner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.Preferences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(unicode: false),
                        Value = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Owner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Preferences", new[] { "Owner_ID" });
            DropIndex("dbo.Albums", new[] { "Owner_ID" });
            DropIndex("dbo.Comments", new[] { "ParentPost_ID" });
            DropIndex("dbo.Comments", new[] { "Owner_ID" });
            DropIndex("dbo.Posts", new[] { "Album_ID" });
            DropIndex("dbo.Posts", new[] { "Owner_ID" });
            DropIndex("dbo.Messages", new[] { "User_ID" });
            DropIndex("dbo.Messages", new[] { "Receiver_ID" });
            DropIndex("dbo.Messages", new[] { "Sender_ID" });
            DropIndex("dbo.Notifications", new[] { "Receiver_ID" });
            DropIndex("dbo.BroRequests", new[] { "User_ID1" });
            DropIndex("dbo.BroRequests", new[] { "User_ID" });
            DropIndex("dbo.BroRequests", new[] { "Receiver_ID" });
            DropIndex("dbo.BroRequests", new[] { "Sender_ID" });
            DropIndex("dbo.Circles", new[] { "User_ID" });
            DropIndex("dbo.Circles", new[] { "Owner_ID" });
            DropIndex("dbo.Likings", new[] { "Profile_ID" });
            DropIndex("dbo.Hobbies", new[] { "Profile_ID" });
            DropIndex("dbo.Users", new[] { "Circle_ID" });
            DropIndex("dbo.Users", new[] { "ID" });
            DropForeignKey("dbo.Preferences", "Owner_ID", "dbo.Users");
            DropForeignKey("dbo.Albums", "Owner_ID", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentPost_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Owner_ID", "dbo.Users");
            DropForeignKey("dbo.Posts", "Album_ID", "dbo.Albums");
            DropForeignKey("dbo.Posts", "Owner_ID", "dbo.Users");
            DropForeignKey("dbo.Messages", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.Users");
            DropForeignKey("dbo.Messages", "Sender_ID", "dbo.Users");
            DropForeignKey("dbo.Notifications", "Receiver_ID", "dbo.Users");
            DropForeignKey("dbo.BroRequests", "User_ID1", "dbo.Users");
            DropForeignKey("dbo.BroRequests", "User_ID", "dbo.Users");
            DropForeignKey("dbo.BroRequests", "Receiver_ID", "dbo.Users");
            DropForeignKey("dbo.BroRequests", "Sender_ID", "dbo.Users");
            DropForeignKey("dbo.Circles", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Circles", "Owner_ID", "dbo.Users");
            DropForeignKey("dbo.Likings", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.Hobbies", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.Users", "Circle_ID", "dbo.Circles");
            DropForeignKey("dbo.Users", "ID", "dbo.Profiles");
            DropTable("dbo.Preferences");
            DropTable("dbo.Albums");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.Messages");
            DropTable("dbo.Notifications");
            DropTable("dbo.BroRequests");
            DropTable("dbo.Circles");
            DropTable("dbo.Likings");
            DropTable("dbo.Hobbies");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
        }
    }
}
