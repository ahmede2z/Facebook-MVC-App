namespace Facebook.Models.ViewModels {
    public class UserFriendsViewModel {
        public _HeaderModel _Header { get; set; }
        public _ProfileThumbModel _ProfileThumb { get; set; }
        public List<UserFriend> Friends { get; set; }
        public List<User> Users { get; set; }
    }
}
