using FileUploadApi.Interfaces;
using FileUploadApi.Requests;
using FileUploadApi.Response;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace FileUploadApi.Controllers
{
    [ApiController]
    
    [Route("api/[controller]/[action]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> logger;
        private readonly IPostService postService;


        public PostsController(ILogger<PostsController> logger, IPostService postService)
        {
            this.logger = logger;
            this.postService = postService;
        }

        [HttpPost]
        [Route("")]
        [RequestSizeLimit(5 * 1024 * 1024)]

        // public IActionResult SubmitPost(PostRequest postRequest)
        public async Task<IActionResult> SubmitPost([FromForm] PostRequest postRequest)
        {
            if (postRequest == null)
            {
                return BadRequest(new PostResponse { Success = false, ErrorCode = "S01", Error = "Invalid post request" });
            }

            if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
            {
                return BadRequest(new PostResponse { Success = false, ErrorCode = "S02", Error = "Invalid post header" });
            }

            if (postRequest.Image != null)
            {
                await postService.SavePostImageAsync(postRequest);
            }

            var postResponse = await postService.CreatePostAsync(postRequest);

            if (!postResponse.Success)
            {
                return NotFound(postResponse);
            }

            return Ok(postResponse.Post);
            //return Ok();

        }


        [HttpGet]
        [Route("")]
        public IActionResult GetEventList()
        {
            try
            {
                var Post = postService.GetEventList();
                if (Post == null)
                    return NotFound();
                return Ok(Post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        //  [HttpPost]
        //  [Route("[action]")]
        //public IActionResult SaveEvent(PostRequest postRequest)
        //{
        //    try
        //    {
        //        var model = postService.SaveEvent(postRequest);
        //        return Ok(model);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}


