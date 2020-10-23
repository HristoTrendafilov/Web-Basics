using SULS.InputModels;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
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
        public HttpResponse Create(AddProblemInputModel problem)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(problem.Name) || problem.Name.Length < 5 || problem.Name.Length > 20)
            {
                return this.Error("Problem name must be between 5 and 20 characters!");
            }

            if(problem.Points < 50 || problem.Points > 300)
            {
                return this.Error("Total points must be between 50 and 300!");
            }

            this.problemService.CreateProblem(problem.Name, problem.Points);
            return this.Redirect("/");
        }

        [HttpGet]
        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.problemService.GetDetails(id);
            return this.View(viewModel);
        }
    }
}
