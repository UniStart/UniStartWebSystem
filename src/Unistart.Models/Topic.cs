namespace Unistart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(90)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
