using FileUploadApi.Entities;
using FileUploadApi.Helpers;
using FileUploadApi.Interfaces;
using FileUploadApi.Requests;
using FileUploadApi.Response;
using FileUploadApi.Response.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileUploadApi.Services
{
    public class PostService : IPostService
    {
        private readonly SocialDbContext socialDbContext;
        private readonly IWebHostEnvironment environment;

        public PostService(SocialDbContext socialDbContext, IWebHostEnvironment environment)
        {
            this.socialDbContext = socialDbContext;
            this.environment = environment;
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest postRequest)
        {
             var post = new Entities.Post
                {



                    //model.Messsage = "Event Update Successfully";
                   
                    Title = postRequest.Title,
                    Type = postRequest.Type,
                    Datepublished = postRequest.Datepublished,
                    Author = postRequest.Author,
                    Status = postRequest.Status,
                    Content = postRequest.Content,
                    Imagepath = postRequest.ImagePath,
                    Ts = DateTime.Now,


                };
            
            

            var postEntry = await socialDbContext.Post.AddAsync(post);

            var saveResponse = await socialDbContext.SaveChangesAsync();

            if (saveResponse < 0)
            {
                return new PostResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
            }

            var postEntity = postEntry.Entity;
            var postModel = new PostModel
            {

                Id = postEntity.Id,
                Title = postEntity.Title,
                Type = postEntity.Type,
                Datepublished = postEntity.Datepublished,
                Author = postEntity.Author,
                Status = postEntity.Status,
                Content = postEntity.Content,
                ImagePath = Path.Combine(postEntity.Imagepath),

                Ts = DateTime.Now,

            };

            return new PostResponse { Success = true, Post = postModel };

        }

        public async Task SavePostImageAsync(PostRequest postRequest)
        {
            
            var uniqueFileName = FileHelper.GetUniqueFileName(postRequest.Image.FileName);
            
            var uploads = Path.Combine(environment.WebRootPath, "UploadImg");
            
            var filePath = Path.Combine(uploads, uniqueFileName);
            
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await postRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            
            postRequest.ImagePath = filePath;

            return;
        }

        public  List<Post> GetEventList()
        {           
            List<Post> Eventlist;
            try
            {

                Eventlist = socialDbContext.Set<Post>().ToList().OrderBy(i => i.Datepublished).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return Eventlist;
        }

        //public PostResponse SaveEvent(PostRequest employeeModel)
        //{
        //    PostResponse model = new PostResponse();
        //    try
        //    {


        //        socialDbContext.Add<PostRequest>(employeeModel);
        //        //model.Messsage = "event Inserted Successfully";

        //        socialDbContext.SaveChanges();
        //       // model.IsSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //model.IsSuccess = false;
        //        //model.Messsage = "Error : " + ex.Message;
        //    }
        //    return model;
        //}
    }
}
