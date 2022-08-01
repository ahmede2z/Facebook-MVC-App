namespace Facebook.Models.ViewModels {
    public class UserProfileViewModel {

        public _HeaderModel _Header { get; set; }
        public _ProfileThumbModel _ProfileThumb { get; set; }
        public User User { get; set; }
        public User CurUser { get; set; }
        public Post Post { get; set; }
        public PostLike Like { get; set; }
        public PostComment Comment { get; set; }
        public _PostModel PostModel { get; set; }

    }
}