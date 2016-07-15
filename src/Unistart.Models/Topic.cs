namespace Unistart.Models
{
    using System;
    using System.Collections.Generic;

    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
