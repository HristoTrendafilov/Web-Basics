namespace Git.Controllers
{
    using Git.Global;
    using Git.InputModels.Repositories;
    using Git.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        [HttpGet]
        public HttpResponse All()
        {
            var viewmodel = this.repositoriesService.GetAllRepositories();
            return this.View(viewmodel);
        }

        [HttpGet]
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryInputModel repositoryModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(repositoryModel.Name) || repositoryModel.Name.Length < GlobalValidations.RepositoryNameMinLength || repositoryModel.Name.Length > GlobalValidations.RepositoryNameMaxLength)
            {
                return this.Error(GlobalErrors.invalidRepositoryName);
            }

            var userId = this.GetUserId();
            this.repositoriesService.CreateRepository(repositoryModel, userId);
            return this.Redirect("/Repositories/All");
        }
    }
}
