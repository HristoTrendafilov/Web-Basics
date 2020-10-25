namespace Git.ViewModels.Repositories
{
    using System;
    using System.Globalization;

    public class AllRepositoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedOnAsSting => this.CreatedOn.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        public int CommitsCount { get; set; }
    }
}
