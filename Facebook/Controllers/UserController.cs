using Facebook.Data;
using Facebook.Models;
using Facebook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Facebook.Controllers {
    public class UserController : Controller {
        private readonly ApplicationDBContext context;
        private IWebHostEnvironment WebHostEnvironment;
        public UserController(ApplicationDBContext cont, IWebHostEnvironment env) {
            context = cont;
            WebHostEnvironment = env;
        }
        public IActionResult Index() {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            List<User> allUsers = context.Users.ToList();
            List<UserFriend> friends = context.UserFriends.Where(f => f.FriendId == user.Id && f.FriendRequestStatus == 0).ToList();
            List<Post> posts = context.Posts.OrderByDescending(e => e.PostDate).Where(p => p.UserId == user.Id).ToList();
            List<PostLike> likes = context.PostLikes.Where(p => p.UserId == user.Id).ToList();
            List<PostLike> postsLike = context.PostLikes.ToList();
            List<PostComment> comments = context.PostComments.ToList();
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
                Post = new Post() {
                    User = user,
                },
                Like = new PostLike(),
                Comment = new PostComment(),
                PostModel = new _PostModel() {
                    Posts = posts,
                    Likes = likes,
                    Comments = comments,
                    Users = allUsers,
                    PostsLikes = postsLike
                }
            };
            return View(userProfile);

        }
        public IActionResult Logout() {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            HttpContext.Session.Remove("UserData");
            return RedirectToAction("Login");
        }
        public IActionResult Login() {
            return View();
        }
        public IActionResult Register() {
            return View();
        }
        public IActionResult EditProfile() {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            List<User> users = context.Users.ToList();
            List<UserFriend> friends = context.UserFriends.Where(f => f.FriendId == user.Id && f.FriendRequestStatus == 0).ToList();
            EditViewModel editViewModel = new EditViewModel() {
                _Header = new _HeaderModel() {
                    User = user,
                    Friends = friends,
                    Users = users
                },
                _ProfileThumb = new _ProfileThumbModel() {
                    user = user,
                    ThumbBtnVis = true
                },
                User = user
            };
            return View(editViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser([Bind("FirstName,SecondName,Email,Password,ConfirmPassword,Phone")] User user) {
            if (ModelState.IsValid) {
                if (context.Users.FirstOrDefault(u => u.Email == user.Email) == null) {
                    user.ProfileCover = "\\img\\defaultCover.jpg";
                    user.ProfileImage = "\\img\\defaultProfile.jpg";

                    context.Users.Add(user);
                    context.SaveChanges();

                    return RedirectToAction("Login");
                }
                ViewBag.User = "Email Already Used.";
            }
            return View("Register", user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetUser([Bind("Email,Password")] User user) {
            User newUser = context.Users.SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (newUser != null) {
                HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(newUser));
                return RedirectToAction("Index");
            }
            ViewBag.User = "User Not Found.";

            return View("Login", user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser([Bind("Id,FirstName,SecondName,Email,Password,ConfirmPassword,Phone,Country,City,ProfileImage,ProfileCover")]
        User user, IFormFile? profImg, IFormFile? profCover, int id) {
            //if (id != user.Id)
            //    return BadRequest();
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            if (ModelState.IsValid) {
                if (profImg != null) {
                    if (user.ProfileImage != "\\img\\defaultProfile.jpg") {
                        string oldImgPath = WebHostEnvironment.WebRootPath + user.ProfileImage;
                        if (System.IO.File.Exists(oldImgPath))
                            System.IO.File.Delete(oldImgPath);
                    }

                    string imgExtenstion = Path.GetExtension(profImg.FileName);
                    Guid guid = Guid.NewGuid();
                    string imgName = guid + imgExtenstion;
                    string imgUrl = "\\img\\" + imgName;
                    user.ProfileImage = imgUrl;

                    string imgPath = WebHostEnvironment.WebRootPath + imgUrl;

                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    profImg.CopyTo(imgStream);
                    imgStream.Dispose();
                } else if (user.ProfileImage == null) {
                    user.ProfileImage = "\\img\\defaultProfile.jpg";
                }

                if (profCover != null) {
                    if (user.ProfileCover != "\\img\\defaultCover.jpg") {
                        string oldImgPath = WebHostEnvironment.WebRootPath + user.ProfileCover;
                        if (System.IO.File.Exists(oldImgPath))
                            System.IO.File.Delete(oldImgPath);
                    }

                    string imgExtenstion = Path.GetExtension(profCover.FileName);
                    Guid guid = Guid.NewGuid();
                    string imgName = guid + imgExtenstion;
                    string imgUrl = "\\img\\" + imgName;
                    user.ProfileCover = imgUrl;

                    string imgPath = WebHostEnvironment.WebRootPath + imgUrl;

                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    profCover.CopyTo(imgStream);
                    imgStream.Dispose();
                } else if (user.ProfileCover == null) {
                    user.ProfileCover = "\\img\\defaultCover.jpg";
                }

                HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(user));
                context.Users.Update(user);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("EditProfile", user);
        }
        public IActionResult MyFriends() {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            List<UserFriend> friends = context.UserFriends.Where(f => f.UserId == user.Id && f.FriendRequestStatus == 1).ToList();
            List<UserFriend> newFriends = context.UserFriends.Where(f => f.FriendId == user.Id && f.FriendRequestStatus == 0).ToList();
            List<User> users = context.Users.ToList();
            UserFriendsViewModel model = new UserFriendsViewModel() {
                Friends = friends,
                Users = users,
                _Header = new _HeaderModel() {
                    User = user,
                    Friends = newFriends,
                    Users = users

                },
                _ProfileThumb = new _ProfileThumbModel() {
                    user = user,
                    ThumbBtnVis = true
                },
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult GetUser(int id, Post? newPost, UserFriend? newfriend) {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            if (id == 0) {
                id = newPost.UserId;
            }
            if (newfriend.FriendId != 0) {
                id = newfriend.FriendId;
            }
            User curUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserData"));
            if (id == curUser.Id) {
                return RedirectToAction("Index");
            }
            User user = context.Users.FirstOrDefault(u => u.Id == id);
            List<User> allUsers = context.Users.ToList();
            List<Post> posts = new List<Post>();
            List<UserFriend> friends = context.UserFriends.ToList();
            List<UserFriend> newFriends = context.UserFriends.Where(f => f.FriendId == curUser.Id && f.FriendRequestStatus == 0).ToList();
            if (context.UserFriends.FirstOrDefault(f => f.UserId == curUser.Id && f.FriendId == id && f.FriendRequestStatus == 1) != null) {
                posts = context.Posts.OrderByDescending(e => e.PostDate).Where(p => p.UserId == user.Id && (p.PostStatus == 1 || p.PostStatus == 2)).ToList();
            } else {
                posts = context.Posts.OrderByDescending(e => e.PostDate).Where(p => p.UserId == user.Id && p.PostStatus == 1).ToList();
            }
            List<PostLike> likes = context.PostLikes.Where(p => p.UserId == curUser.Id).ToList();
            List<PostComment> comments = context.PostComments.ToList();
            List<PostLike> postsLike = context.PostLikes.ToList();
            UserProfileViewModel userProfile = new UserProfileViewModel() {
                _Header = new _HeaderModel() {
                    User = curUser,
                    Friends = newFriends,
                    Users = allUsers
                },
                _ProfileThumb = new _ProfileThumbModel() {
                    user = user,
                    CurUser = curUser,
                    Users = allUsers,
                    Friends = friends,
                    ThumbBtnVis = false
                },

                User = user,
                CurUser = curUser,
                Post = new Post() {
                    User = user,
                },
                Like = new PostLike(),
                Comment = new PostComment(),
                PostModel = new _PostModel() {
                    Posts = posts,
                    Likes = likes,
                    Comments = comments,
                    Users = allUsers,
                    PostsLikes = postsLike
                }
            };
            return View(userProfile);
        }
        public IActionResult SendRequest(int friendId, int userId) {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            UserFriend friend = new UserFriend() {
                UserId = userId,
                FriendId = friendId,
                FriendRequestStatus = 0,
            };
            context.UserFriends.Add(friend);
            context.SaveChanges();
            return RedirectToAction("GetUser", friend);
        }
        public IActionResult AddFriend(int friendId, int userId) {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            UserFriend oldFriend = context.UserFriends.FirstOrDefault(x => x.UserId == friendId &&
            x.FriendId == userId && x.FriendRequestStatus == 0);
            oldFriend.FriendRequestStatus = 1;
            UserFriend friend = new UserFriend() {
                UserId = userId,
                FriendId = friendId,
                FriendRequestStatus = 1,
            };
            context.UserFriends.Add(friend);
            context.SaveChanges();
            context.UserFriends.Update(oldFriend);
            context.SaveChanges();
            return RedirectToAction("GetUser", friend);
        }
        public IActionResult CancelRequest(int friendId, int userId) {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            UserFriend friend = context.UserFriends.FirstOrDefault(x => x.UserId == userId &&
            x.FriendId == friendId && x.FriendRequestStatus == 0);
            context.UserFriends.Remove(friend);
            context.SaveChanges();
            return RedirectToAction("GetUser", friend);
        }
        public IActionResult RemoveFriend(int friendId, int userId) {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            UserFriend userFriend = context.UserFriends.FirstOrDefault(x => x.UserId == userId &&
              x.FriendId == friendId && x.FriendRequestStatus == 1);
            UserFriend friend = context.UserFriends.FirstOrDefault(x => x.UserId == friendId &&
            x.FriendId == userId && x.FriendRequestStatus == 1);
            context.UserFriends.Remove(friend);
            context.SaveChanges();
            context.UserFriends.Remove(userFriend);
            context.SaveChanges();
            return RedirectToAction("GetUser", userFriend);
        }
        public IActionResult Decline(int friendId, int userId) {
            if (HttpContext.Session.GetString("UserData") == null)
                return RedirectToAction("Login");
            UserFriend friend = context.UserFriends.FirstOrDefault(x => x.UserId == userId &&
            x.FriendId == friendId && x.FriendRequestStatus == 0);
            UserFriend userFriend = new UserFriend() {
                UserId = friend.FriendId,
                FriendId = friend.UserId,
                FriendRequestStatus = friend.FriendRequestStatus,
            };
            context.UserFriends.Remove(friend);
            context.SaveChanges();

            return RedirectToAction("GetUser", userFriend);
        }
    }
}
