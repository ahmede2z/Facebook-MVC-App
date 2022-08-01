using Facebook.Data;
using Facebook.Models;
using Facebook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Facebook.Controllers {
    public class PostsController : Controller {
        private readonly ApplicationDBContext context;
        private IWebHostEnvironment WebHostEnvironment;
        public PostsController(ApplicationDBContext cont, IWebHostEnvironment env) {
            context = cont;
            WebHostEnvironment = env;
        }
        public IActionResult Index() {
            return RedirectToAction("Index", "User");
        }
        [HttpPost]
        public IActionResult CreatePost(Post post, IFormFile? postImg) {

            if (post.PostContent == null && postImg == null) {
                return RedirectToAction("Index", "User");

            }
            if (postImg != null) {
                string imgExtenstion = Path.GetExtension(postImg.FileName);
                Guid guid = Guid.NewGuid();
                string imgName = guid + imgExtenstion;
                string imgUrl = "\\img\\" + imgName;
                post.PostImage = imgUrl;
                string imgPath = WebHostEnvironment.WebRootPath + imgUrl;
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                postImg.CopyTo(imgStream);
                imgStream.Dispose();
            }
            //post.PostId = post.UserId + context.Posts.ToList().Count();
            context.Posts.Add(post);
            context.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public IActionResult EditPost(int postId) {
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            List<UserFriend> friends = context.UserFriends.Where(f => f.FriendId == user.Id && f.FriendRequestStatus == 0).ToList();
            List<User> allUsers = context.Users.ToList();
            Post post = context.Posts.SingleOrDefault(p => p.PostId == postId);
            UserProfileViewModel userProfile = new UserProfileViewModel() {
                _Header = new _HeaderModel() {
                    User = user,
                    Friends = friends,
                    Users = allUsers
                },
                _ProfileThumb = new _ProfileThumbModel() {
                    user = user,
                    ThumbBtnVis = true
                },
                User = user,
                CurUser = user,
                Post = post
            };
            return View("EditPost", userProfile);
        }
        [HttpPost]
        public IActionResult EditThePost(Post post) {

            context.Posts.Update(post);
            context.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        public IActionResult DeletePost(int postId) {
            Post post = context.Posts.Find(postId);
            string oldImgPath = WebHostEnvironment.WebRootPath + post.PostImage;
            if (System.IO.File.Exists(oldImgPath))
                System.IO.File.Delete(oldImgPath);
            context.Posts.Remove(post);
            context.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        [HttpPost]
        public IActionResult React(PostLike like, bool likeStatus) {
            User curUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            Post post = context.Posts.FirstOrDefault(p => p.PostId == like.PostId);
            PostLike postLike = context.PostLikes.SingleOrDefault(lid => lid.PostId == like.PostId && lid.UserId == like.UserId);
            if (postLike == null) {
                like.UserId = like.UserId;
                like.PostId = like.PostId;
                like.LikeStatus = likeStatus;
                if (likeStatus == true) {
                    post.NumOfLike = post.NumOfLike + 1;
                } else if (likeStatus == false) {
                    post.NumOfDisLike = post.NumOfDisLike + 1;
                }
                context.PostLikes.Add(like);
                context.Posts.Update(post);
                context.SaveChanges();
            } else if (postLike.LikeStatus == likeStatus) {
                if (likeStatus == true) {
                    post.NumOfLike = post.NumOfLike - 1;
                } else if (likeStatus == false) {
                    post.NumOfDisLike = post.NumOfDisLike - 1;
                }
                context.PostLikes.Remove(postLike);
                context.Posts.Update(post);
                context.SaveChanges();
            } else {
                if (likeStatus == true) {
                    post.NumOfDisLike = post.NumOfDisLike - 1;
                    post.NumOfLike = post.NumOfLike + 1;

                } else if (likeStatus == false) {
                    post.NumOfLike = post.NumOfLike - 1;
                    post.NumOfDisLike = post.NumOfDisLike + 1;
                }
                postLike.LikeStatus = likeStatus;
                context.PostLikes.Update(postLike);
                context.Posts.Update(post);
                context.SaveChanges();
            }

            if (post.UserId == curUser.Id)
                return RedirectToAction("Index", "User");
            else {
                return RedirectToAction("GetUser", "User", post);
            }
        }
        [HttpPost]
        public IActionResult AddComment(PostComment comment) {
            User curUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            Post post = context.Posts.FirstOrDefault(p => p.PostId == comment.PostId);
            if (comment.CommentText == null) {
                return RedirectToAction("Index", "User");
            }
            post.NumOfComment = post.NumOfComment + 1;
            context.PostComments.Add(comment);
            context.Posts.Update(post);
            context.SaveChanges();

            if (post.UserId == curUser.Id)
                return RedirectToAction("Index", "User");
            else {
                return RedirectToAction("GetUser", "User", post);
            }
        }

    }

}


