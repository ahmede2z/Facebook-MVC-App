namespace Facebook.Models {
    public class PostLike {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool LikeStatus { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }

    }
}
