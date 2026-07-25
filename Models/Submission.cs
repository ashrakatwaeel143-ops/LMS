using System;

namespace LmsProject.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public string Content { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public int? Grade { get; set; }
    }
}