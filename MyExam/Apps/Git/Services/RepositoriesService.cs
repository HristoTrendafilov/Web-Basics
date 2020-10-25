namespace Git.Services
{
    using Git.Data;
    using Git.Data.Models;
    using Git.InputModels.Repositories;
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;
    using System.Linq;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateRepository(CreateRepositoryInputModel repositoryModel, string userId)
        {
            var repository = new Repository()
            {
                Name = repositoryModel.Name,
                IsPublic = repositoryModel.RepositoryType == "Public" ? true : false,
                OwnerId = userId
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public IEnumerable<AllRepositoryViewModel> GetAllRepositories()
        {
            return this.db.Repositories
                .Where(x => x.IsPublic)
                .Select(x => new AllRepositoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CommitsCount = x.Commits.Count(),
                    CreatedOn = x.CreatedOn,
                    Owner = x.Owner.Username
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }
    }
}
