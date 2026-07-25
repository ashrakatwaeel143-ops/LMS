namespace LmsProject.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}