namespace Git.Controllers
{
    using Git.Global;
    using Git.InputModels.Commits;
    using Git.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        [HttpGet]
        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.commitsService.GetRepositoryInfo(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateCommitInputModel commitModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(commitModel.Description) || commitModel.Description.Length < GlobalValidations.CommitDescriptionMinLength)
            {
                return this.Error(GlobalErrors.invalidCommitDescription);
            }

            var userId = this.GetUserId();
            this.commitsService.CreateCommit(commitModel, userId);
            return this.Redirect("/Repositories/All");
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            var viewModel = this.commitsService.GetAllCommitsByUser(userId);
            return this.View(viewModel);
        }

        [HttpGet]
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.commitsService.DeleteCommit(id);
            return this.Redirect("/Commits/All");
        }
    }
}
