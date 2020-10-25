namespace Git.Services
{
    using Git.InputModels.Commits;
    using Git.ViewModels.Commits;
    using System.Collections.Generic;

    public interface ICommitsService
    {
        CreateCommitViewModel GetRepositoryInfo(string repositoryId);

        void CreateCommit(CreateCommitInputModel commitModel, string userId);

        IEnumerable<AllUserCommitsViewModel> GetAllCommitsByUser(string userId);

        void DeleteCommit(string commitId);
    }
}
