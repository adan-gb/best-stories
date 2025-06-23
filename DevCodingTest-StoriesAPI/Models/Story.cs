namespace DevCodingTest_StoriesAPI.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? By { get; set; }
        public int Time { get; set; }
        public DateTime? TimeFormat { get; set; }
        public int CommentCount { get; set; }
        public int Score { get; set; }
    }
}
