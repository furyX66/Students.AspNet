namespace Students.Common.Models
{
    public class LectureHall
    {
        public int Id { get; set; }
        public string HallNumber { get; set; } = string.Empty;
        public int Floor { get; set; }

        #region ctors

        public LectureHall(int id, string hallNumber, int floor)
        {
            Id = id;
            HallNumber = hallNumber;
            Floor = floor;
        }

        public LectureHall()
        {
        }

        #endregion ctors
    }
}