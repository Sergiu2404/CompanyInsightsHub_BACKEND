using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.dtos.comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title is too short, it must be of length >= 3")]
        [MaxLength(255, ErrorMessage = "Title is too long, it must be of length <= 255")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000, ErrorMessage = "Content is too long, it must be of length <= 1000")]
        public string Content { get; set; } = string.Empty;
    }
}