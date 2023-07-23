using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Text;
using Taskapp.Models;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using FileUploadApi.Entities;
using FileUploadApi.Response.Models;
using FileUploadApi.Response;
using FileUploadApi.Requests;
using FileUploadApi.Services;
using FileUploadApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using FileUploadApi.Controllers;

namespace Taskapp.Controllers
{
    public class EventController : Controller
    {
        Uri bassaddress = new Uri("https://localhost:7296/api/Posts");
        private readonly HttpClient _Client;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SocialDbContext socialDbContext;
        private  IPostService postService;
        MasterFunctions obj = new MasterFunctions();
        public EventController(IWebHostEnvironment hostEnvironment )
        {
            _Client = new HttpClient();
            _Client.BaseAddress = bassaddress;
            webHostEnvironment = hostEnvironment;
            
        }
        [HttpGet]
        public IActionResult Index()
        {
           // var employee = await socialDbContext.Post.ToListAsync();
            // int? page = 1;
            List<EventViewModel> Eventlist = new List<EventViewModel>();
            HttpResponseMessage responseMessage = _Client.GetAsync(_Client.BaseAddress + "/GetEventList").Result;
           
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                Eventlist = JsonConvert.DeserializeObject<List<EventViewModel>>(data);
                
            }
           // var Eventlist2= GetPage(Eventlist, 1, 1);
            // return View(Eventlist.to.ToPagedList(pageNumber, pageSize));
            return View(Eventlist);
        }
        IList<EventViewModel> GetPage(IList<EventViewModel> list, int page, int pageSize)
        {
            return list.Skip(page * pageSize).Take(pageSize).ToList();
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }


        //[HttpPost]
        //public IActionResult Createnew(EventViewModel _Events)
        //{
        //    try
        //    {
        //        string uniqueFileName = UploadedFile(_Events);
        //        var _post = new Post
        //        {
        //            Title = _Events.Title,
        //            Type = _Events.Type,
        //            Datepublished = _Events.Datepublished,
        //            Author = _Events.Author,
        //            Status = _Events.Status,
        //            Content = _Events.Content,
        //            Imagepath = uniqueFileName,
        //            Ts = DateTime.Now,


        //        };
        //        string data = JsonConvert.SerializeObject(_post);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "/SaveEvent", content).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["successmessage"] = "Event Saved";
        //            return RedirectToAction("Index");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["errormessage"] = ex.Message;
        //        return View();
        //    }

        //    return View();

        //}
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] EventViewModel _Events,PostRequest p)
        {
            //try
            ////{
           

                string data = JsonConvert.SerializeObject(_Events);               
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "/SubmitPost", content).Result;
                string uniqueFileName= UploadedFile( _Events);               
                //var model = postService.SaveEvent(p);
            // return Ok(model);
            SqlParameter[] oPara =
                  {
               
                new SqlParameter("@Title", SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar),
                   new SqlParameter("@Author",SqlDbType.VarChar),
                      new SqlParameter("@Content",SqlDbType.VarChar),
                      new SqlParameter("@Image",SqlDbType.VarChar),
                      new SqlParameter("@Imagepath",SqlDbType.NVarChar),
                       new SqlParameter("@Status",SqlDbType.Bit),
                        new SqlParameter("@Datepublished",SqlDbType.DateTime),                     
                      
                  new SqlParameter("@Rval", SqlDbType.Int),
                    };

            oPara[0].Value = p.Title;
            oPara[1].Value = p.Type;
            oPara[2].Value = p.Author;
            oPara[3].Value = p.Content;
            oPara[4].Value = "";
            oPara[5].Value = uniqueFileName;
            oPara[6].Value = p.Status;
            oPara[7].Value = p.Datepublished;

            oPara[8].Direction = ParameterDirection.ReturnValue;
            obj.ExecuteSp("SaveData", oPara, CommandType.StoredProcedure);


            return RedirectToAction(nameof(Index));
           
           // return View();

        }
        private string UploadedFile(EventViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "UploadImg");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
