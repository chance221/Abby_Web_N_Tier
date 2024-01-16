﻿using System.ComponentModel.DataAnnotations;

namespace Abby.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Display Order")]
        [Range(1, 100, ErrorMessage = "Must Be in Range of 1-100")]
        public int DisplayOrder { get; set; }
    }
}
