using SharedTrip.InputModels;
using Suls.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Controllers
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
                return this.Redirect("/Trips/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserInputModel userModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Trips/All");
            }

            var userId = this.usersService.GetUserId(userModel.Username, userModel.Password);

            if(userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Trips/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserInputModel userModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Trips/All");
            }

            if (string.IsNullOrEmpty(userModel.Username) || userModel.Username.Length < 5 || userModel.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters!");
            }

            if (!this.usersService.IsUsernameAvailable(userModel.Username))
            {
                return this.Error("Username is already Taken!");
            }

            if (string.IsNullOrEmpty(userModel.Email) || !new EmailAddressAttribute().IsValid(userModel.Email))
            {
                return this.Error("Invalid email address!");
            }

            if (!this.usersService.IsEmailAvailable(userModel.Email))
            {
                return this.Error("Email is already taken!");
            }

            if (string.IsNullOrEmpty(userModel.Password) || userModel.Password.Length < 6 || userModel.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters!");
            }

            if (userModel.Password != userModel.ConfirmPassword)
            {
                return this.Error("Passwords don't match!");
            }

            this.usersService.CreateUser(userModel.Username, userModel.Email, userModel.Password);
            return this.Redirect("/Users/Login");
        }

        [HttpGet]
        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                this.Error("Only logged in users can logout!");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
