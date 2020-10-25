namespace Git.Services
{
    using Git.InputModels.Repositories;
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;

    public interface IRepositoriesService
    {
        void CreateRepository(CreateRepositoryInputModel repositoryModel, string userId);

        IEnumerable<AllRepositoryViewModel> GetAllRepositories();
    }
}
