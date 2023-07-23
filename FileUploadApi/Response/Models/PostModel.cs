﻿using System.Text.Json.Serialization;

namespace FileUploadApi.Response.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Author { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? ImagePath { get; set; }
        

        public bool Status { get; set; }

        public DateTime Datepublished { get; set; }
        public DateTime Ts { get; set; }
    }
}