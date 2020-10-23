using SULS.ViewModels;
using System.Collections.Generic;

namespace SULS.Services
{
    public interface IProblemService
    {
        void CreateProblem(string name, int points);

        IEnumerable<AllProblemsViewModel> GetAll();

        ProblemDetailsViewModel GetDetails(string problemId);
    }
}
