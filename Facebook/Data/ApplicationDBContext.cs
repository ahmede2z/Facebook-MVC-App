using Facebook.Models;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Data {
    public class ApplicationDBContext : DbContext {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<UserFriend>()
                .HasKey(b => new { b.UserId, b.FriendId });
            modelBuilder.Entity<PostLike>()
                .HasKey(b => new { b.UserId, b.PostId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

    }
}
