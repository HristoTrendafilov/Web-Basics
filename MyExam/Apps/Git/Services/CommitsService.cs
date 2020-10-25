namespace Git.Services
{
    using Git.Data;
    using Git.Data.Models;
    using Git.InputModels.Commits;
    using Git.ViewModels.Commits;
    using System.Collections.Generic;
    using System.Linq;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateCommit(CreateCommitInputModel commitModel, string userId)
        {
            var commit = new Commit()
            {
                CreatorId = userId,
                Description = commitModel.Description,
                RepositoryId = commitModel.Id
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public void DeleteCommit(string commitId)
        {
            var commit = this.db.Commits.FirstOrDefault(x => x.Id == commitId);

            if(commit == null)
            {
                return;
            }

            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }

        public IEnumerable<AllUserCommitsViewModel> GetAllCommitsByUser(string userId)
        {
            return this.db.Commits.Where(x => x.CreatorId == userId)
                .Select(x => new AllUserCommitsViewModel()
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Description = x.Description,
                    Repository = x.Repository.Name
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }

        public CreateCommitViewModel GetRepositoryInfo(string repositoryId)
        {
            var repositoryName = this.db.Repositories.Find(repositoryId).Name;

            return new CreateCommitViewModel()
            {
                Name = repositoryName,
                Id = repositoryId
            };
        }
    }
}
