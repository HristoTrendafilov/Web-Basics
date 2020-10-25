namespace Git.ViewModels.Commits
{
    using System;
    using System.Globalization;

    public class AllUserCommitsViewModel
    {
        public string Id { get; set; }

        public string Repository { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedOnAsString => this.CreatedOn.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
