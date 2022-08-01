namespace Facebook.Models.ViewModels {
    public class _ProfileThumbModel {
        public List<User> Users { get; set; }
        public List<UserFriend> Friends { get; set; }
        public User user { get; set; }
        public User CurUser { get; set; }

        public bool ThumbBtnVis { get; set; }
    }
}
