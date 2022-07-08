using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public record Comment
    {
        public Guid Id { get; set; }

        [Required]        
        public Guid PostId { get; set; }

        [Required]
        [StringLength(120)]
        public string Content { get; set; }

        [StringLength(30)]
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
    }
}