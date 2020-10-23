using SULS.InputModels;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SULS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(username,password);

            if(userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserInputModel user)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 5 || user.Username.Length > 20)
            {
                return this.Error("Username must be between 5 and 20 characters!");
            }

            if (!this.usersService.IsUsernameAvailable(user.Username))
            {
                return this.Error("Username is already taken!");
            }

            if(string.IsNullOrEmpty(user.Email) || !new EmailAddressAttribute().IsValid(user.Email))
            {
                return this.Error("Invalid email address!");
            }

            if (!this.usersService.IsEmailAvailable(user.Email))
            {
                return this.Error("Email is already taken!");
            }

            if(string.IsNullOrEmpty(user.Password) || user.Password.Length < 6 || user.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters!");
            }

            if(user.Password != user.ConfirmPassword)
            {
                return this.Error("Passwords don't match!");
            }

            this.usersService.CreateUser(user.Username, user.Email, user.Password);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
