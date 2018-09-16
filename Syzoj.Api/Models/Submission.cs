using System.ComponentModel.DataAnnotations;

namespace Syzoj.Api.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        public int ProblemsetId { get; set; }
        public virtual Problemset Problemset { get; set; }
        public int ProblemId { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual ProblemsetProblem ProblemsetProblem { get; set; }
        public string Path { get; set; }
        [Required]
        public byte[] Summary { get; set; }
        [Required]
        public byte[] Content { get; set; }
    }
}