using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Models {
    public class User {
        [Display(Name = "Student ID")]
        public int Id { get; set; }

        [Display(Name = "First Name", Prompt = "First Name", Description = "Your first name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name", Prompt = "Last Name", Description = "Your last name")]
        [Required(ErrorMessage = "SecondName is required.")]
        public string SecondName { get; set; }

        [Display(Name = "E-mail Address", Prompt = "Email", Description = "Email of the account")]
        [Required(ErrorMessage = "E-mail Address is required")]
        [EmailAddress(ErrorMessage = "You must provide a Valid Email Address!")]
        public string Email { get; set; }

        [Display(Name = "Password", Prompt = "Password", Description = "Password of the account")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Confirm Password", Prompt = "Password Again", Description = "Confirmation Password of the account")]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number", Prompt = "Phone Number", Description = "Phone of the account")]
        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "You must provide a valid Phone number!")]
        public string Phone { get; set; }

        [Display(Name = "Country", Prompt = "Country", Description = "Your Country")]
        public string? Country { get; set; }

        [Display(Name = "City", Prompt = "City", Description = "Your City")]
        public string? City { get; set; }

        [Display(Name = "Profile Image", Prompt = "Profile Image", Description = "Profile Image of the account")]
        public string? ProfileImage { get; set; }

        [Display(Name = "Profile Cover", Prompt = "Profile Cover", Description = "Profile Cover of the account")]
        public string? ProfileCover { get; set; }

        public List<UserFriend>? UserFriends { get; set; }
        public List<Post>? Posts { get; set; }
        public List<PostLike>? Likes { get; set; }
        public List<PostComment>? Comments { get; set; }

    }
}
