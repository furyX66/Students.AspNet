namespace Students.Common.Models
{
    public class FieldOfStudies
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public FieldOfStudies(string name)
        {
            Name = name;
        }

        public FieldOfStudies()
        {
        }
    }
}