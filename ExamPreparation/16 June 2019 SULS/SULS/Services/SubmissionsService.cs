using SULS.Data;
using SULS.Data.Models;
using System;
using System.Linq;

namespace SULS.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly ApplicationDbContext db;
        private readonly Random random;

        public SubmissionsService(ApplicationDbContext db, Random random)
        {
            this.db = db;
            this.random = random;
        }

        public string GetProblemName(string problemId)
        {
            return this.db.Problems
                .Where(x => x.Id == problemId)
                .Select(x => x.Name)
                .FirstOrDefault();
        }

        public void CreateSubmission(string code, string problemId, string userId)
        {
            var problem = this.db.Problems.FirstOrDefault(x => x.Id == problemId);

            var submission = new Submission()
            {
                Code = code,
                AchievedResult = this.random.Next(0, problem.Points + 1),
                CreatedOn = DateTime.UtcNow,
                ProblemId = problem.Id,
                UserId = userId
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public void DeleteSubmission(string id)
        {
            var submission = this.db.Submissions.FirstOrDefault(x => x.Id == id);
            this.db.Submissions.Remove(submission);
            this.db.SaveChanges();
        }
    }
}
