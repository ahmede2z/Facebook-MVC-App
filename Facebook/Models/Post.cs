using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Models {
    public class Post {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? PostContent { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
        public int PostStatus { get; set; } = 1;
        public string? PostImage { get; set; }
        public int NumOfLike { get; set; } = 0;
        public int NumOfDisLike { get; set; } = 0;
        public int NumOfComment { get; set; } = 0;
        public List<PostLike>? Likes { get; set; }
        public List<PostComment>? Comments { get; set; }
        public User User { get; set; }
    }
}
