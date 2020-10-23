using SULS.Data;
using SULS.Data.Models;
using SULS.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly ApplicationDbContext db;

        public ProblemService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateProblem(string name, int points)
        {
            var problem = new Problem()
            {
                Name = name,
                Points = points
            };

            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }

        public IEnumerable<AllProblemsViewModel> GetAll()
        {
            return this.db.Problems.Select(x => new AllProblemsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Count = x.Submissions.Count()
            }).ToList();
        }

        public ProblemDetailsViewModel GetDetails(string problemId)
        {
            return this.db.Problems
                .Where(x => x.Id == problemId)
                .Select(x => new ProblemDetailsViewModel()
                {
                    Name = x.Name,
                    Submission = x.Submissions.Select(s => new ProblemSubmission()
                    {
                        AchievedResult = s.AchievedResult,
                        CreatedOn = s.CreatedOn.ToShortDateString(),
                        MaxPoints = x.Points,
                        SubmissionId = s.Id,
                        Username = s.User.Username
                    }).ToList()
                }).FirstOrDefault();
        }
    }
}
