﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileUploadApi.Entities
{
    public partial class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event  is required")]
        [StringLength(200, ErrorMessage = "Event can't be longer than 200 characters")]
        public string Title { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage = "Date publish  is required")]
        public DateTime Datepublished { get; set; }
        public string Author { get; set; }
        
        public bool Status { get; set; }
        [Required(ErrorMessage = "Content  is required")]
        public string Content { get; set; }

        
        public string Imagepath { get; set; }
        public DateTime Ts { get; set; }     
      
    }
}