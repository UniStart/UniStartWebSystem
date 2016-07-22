namespace Unistart.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Lecture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(90)] 
        public string Title { get; set; }
        public DateTime DatePublished { get; set; }
        public string VideoUrl { get; set; }
        public byte[] Presentation { get; set; }
        public byte[] TextFile { get; set; }

        public bool IsDeleted { get; set; }
     }
}
