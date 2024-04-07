namespace Students.Common.Models
{
    public class FieldOfStudies
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new List<Student>();

        public FieldOfStudies(string name)
        {
            Name = name;
        }

        public FieldOfStudies()
        {
        }
    }
}