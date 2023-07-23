using FileUploadApi.Requests;
using FileUploadApi.Response;
using System.Collections.Generic;
using FileUploadApi.Entities;

using System.Collections.Generic;

namespace FileUploadApi.Interfaces
{
    public interface IPostService
    {
        Task SavePostImageAsync(PostRequest postRequest);
       // PostResponse SavePostImageAsync(PostRequest postRequest);
        Task<PostResponse> CreatePostAsync(PostRequest postRequest);

        //PostResponse CreatePostAsync(PostRequest postRequest);
        List<Post> GetEventList();

        //PostResponse SaveEvent(PostRequest employeeModel);

    }
}