using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Syzoj.Api.Models
{
    public class Problem
    {
        [Key]
        public Guid Id { get; set; }
        public string ProblemType { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        [Required]
        public byte[] Statement { get; set; }
        /// <summary>
        /// Optional information used for judging problems.
        /// </summary>
        public byte[] Metadata { get; set; }
        public bool IsSubmittable { get; set; }
    }
}