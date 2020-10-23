using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SULS.Data.Models
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(800)]
        public string Code { get; set; }

        [Required]
        public int AchievedResult { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Problem))]
        public string ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
