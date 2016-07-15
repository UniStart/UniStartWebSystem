namespace Unistart.Models
{
    using System;

    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DatePublished { get; set; }
        public string VideoUrl { get; set; }
        public byte[] Presentation { get; set; }
        public byte[] TextFile { get; set; }
     }
}
