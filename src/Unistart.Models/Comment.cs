namespace Unistart.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
