namespace Facebook.Models.ViewModels {
    public class _PostModel {
        public List<User> Users { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostLike> Likes { get; set; }
        public List<PostLike> PostsLikes { get; set; }
        public List<PostComment> Comments { get; set; }
    }
}
