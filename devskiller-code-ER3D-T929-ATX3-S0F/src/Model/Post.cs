using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public record Post
    {
        [Required]        
        public Guid Id { get; set; }

        
        [StringLength(30, ErrorMessage = "Lenght")]
        public string Title { get; set; }
        
        [StringLength(1200)]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}