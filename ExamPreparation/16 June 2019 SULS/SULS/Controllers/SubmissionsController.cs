using SULS.Services;
using SULS.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;

        public SubmissionsController(ISubmissionsService submissionsService)
        {
            this.submissionsService = submissionsService;
        }

        [HttpGet]
        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CreateSubmissionViewModel()
            {
                ProblemId = id,
                Name = this.submissionsService.GetProblemName(id)
            };
            
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string code, string problemId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(code) || code.Length < 30 || code.Length > 800)
            {
                return this.Error("Code should be between 30 and 800 characters");
            }

            var userId = this.GetUserId();
            this.submissionsService.CreateSubmission(code, problemId, userId);
            return this.Redirect("/");
        }

        [HttpGet]
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.submissionsService.DeleteSubmission(id);
            return this.Redirect("/");
        }
    }
}
