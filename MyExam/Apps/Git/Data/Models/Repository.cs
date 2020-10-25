namespace Git.Data.Models
{
    using Git.Global;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Repository
    {
        public Repository()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Commits = new HashSet<Commit>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(GlobalValidations.RepositoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public bool IsPublic { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}
