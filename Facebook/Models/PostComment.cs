using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Models {
    public class PostComment {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentText { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }

    }
}
