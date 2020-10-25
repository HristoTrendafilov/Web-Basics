namespace Git.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Commit
    {
        public Commit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }
        public User Creator { get; set; }

        [ForeignKey(nameof(Repository))]
        public string RepositoryId { get; set; }
        public Repository Repository { get; set; }
    }
}
