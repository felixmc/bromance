using Bros.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bros.DataModel
{
    public class BroContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<BroRequest> BroRequests { get; set; }
        public DbSet<Circle> Circles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Liking> Likings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<TextPost> TextPosts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}