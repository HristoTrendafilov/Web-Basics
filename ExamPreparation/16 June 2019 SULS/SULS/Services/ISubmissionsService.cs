namespace SULS.Services
{
    public interface ISubmissionsService
    {
        string GetProblemName(string problemId);

        void CreateSubmission(string code, string problemId, string userId);

        void DeleteSubmission(string id);
    }
}
