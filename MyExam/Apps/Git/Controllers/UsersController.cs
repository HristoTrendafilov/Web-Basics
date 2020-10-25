namespace Git.Controllers
{
    using Git.Global;
    using Git.InputModels.Users;
    using Git.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.ComponentModel.DataAnnotations;

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
                return this.Redirect("/Repositories/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserInputModel userModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            var userId = this.usersService.GetUserId(userModel.Username, userModel.Password);

            if(userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);
            return this.Redirect("/Repositories/All");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserInputModel userModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            if (string.IsNullOrEmpty(userModel.Username) || userModel.Username.Length < GlobalValidations.UsernameMinLength || userModel.Username.Length > GlobalValidations.UsernameMaxLength)
            {
                return this.Error(GlobalErrors.invalidUsername);
            }

            if (!this.usersService.IsUsernameAvailable(userModel.Username))
            {
                return this.Error(GlobalErrors.usernameAlreadyTaken);
            }

            if(string.IsNullOrEmpty(userModel.Email) || !new EmailAddressAttribute().IsValid(userModel.Email))
            {
                return this.Error(GlobalErrors.invalidEmail);
            }

            if (!this.usersService.IsEmailAvailable(userModel.Email))
            {
                return this.Error(GlobalErrors.emailAlreadyTaken);
            }

            if(string.IsNullOrEmpty(userModel.Password) || userModel.Password.Length < GlobalValidations.PasswordMinLength || userModel.Password.Length > GlobalValidations.PasswordMaxLength)
            {
                return this.Error(GlobalErrors.invalidPassword);
            }

            if(userModel.Password != userModel.ConfirmPassword)
            {
                return this.Error(GlobalErrors.passwordsDontMatch);
            }

            this.usersService.CreateUser(userModel.Username, userModel.Email, userModel.Password);
            return this.Redirect("/Users/Login");
        }

        [HttpGet]
        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error(GlobalErrors.invalidLogout);
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
