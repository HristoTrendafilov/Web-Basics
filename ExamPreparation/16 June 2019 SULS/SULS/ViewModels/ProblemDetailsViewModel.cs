using System.Collections.Generic;

namespace SULS.ViewModels
{
    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ProblemSubmission> Submission { get; set; }
    }

    public class ProblemSubmission
    {
        public string Username { get; set; }

        public string CreatedOn { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public string SubmissionId { get; set; }
    }
}
