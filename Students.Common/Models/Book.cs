namespace Students.Common.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Book()
        {
        }

        public Book(string title, string author, string description)
        {
            Title = title;
            Author = author;
            Description = description;
        }
    }
}