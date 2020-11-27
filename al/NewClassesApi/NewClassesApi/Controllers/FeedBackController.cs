using Microsoft.AspNetCore.Mvc;
using NewClassesApi.Model.Entity;
using NewClassesApi.Model.QM;
using NewClassesApi.Model.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassesApi.Controllers
{
    [ApiController]
    [Route("Api/FeedBack")]
    public class FeedBackController : ControllerBase
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        NewClassesDbContext _newClassesDbContext;
        //public 
        public FeedBackController(NewClassesDbContext newClassesDbContext)
        {
            this._newClassesDbContext = newClassesDbContext;
        }

        [HttpGet("Test")]
        public string Test([FromQuery] string test)
        {

            //IActionResult
            //return Json(list);

            return $"Hello:{test}";
        }


        [HttpGet("GetFeedBackList")]
        public List<FeedBack> GetFeedBackList([FromQuery] FeedBackQM feedBackQM)
        {
            List<FeedBack> list = new List<FeedBack>();
            list = _newClassesDbContext.FeedBack.ToList();
            return list;
        }



        [HttpPost("AddFeedBack")]
        public MessageResult<FeedBack> AddFeedBack([FromBody] FeedBack model)
        {
            //List<FeedBack> list = new List<FeedBack>();

            //list = _newClassesDbContext.FeedBack.ToList();

            return null;
        }

        [HttpPost("Add")]
        public MessageResult<FeedBack> Add()
        {
            MessageResult<FeedBack> messageResult = new MessageResult<FeedBack>();
            try
            {
                _nLog.Info("开始上传！");

                var files = this.HttpContext.Request.Form.Files;
                var param = this.HttpContext.Request.Form.Keys.ToList();
                string val = "";
                var suggestion=   this.HttpContext.Request.Form[param[0]];
                var phone = this.HttpContext.Request.Form[param[1]];
                //var host = this.HttpContext.Request.Host;
                var directory = Path.Combine(Directory.GetCurrentDirectory(), $"UpLoad\\{DateTime.Now.ToString("yyyy-MM-dd")}");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                List<string> fileNameList = new List<string>();
                foreach (var file in files)
                {
                    var fullName = Path.Combine(directory, file.FileName);
                    using (var fs = System.IO.File.Create(fullName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    fileNameList.Add(fullName);
                }
                string fileNames = string.Join(';', fileNameList);
                FeedBack feedBack = new FeedBack();
                feedBack.Suggestion = suggestion;
                feedBack.Phone = phone;
                feedBack.ImagePath = fileNames;
                this._newClassesDbContext.FeedBack.Add(feedBack);
                this._newClassesDbContext.SaveChanges();

                messageResult.Success = true;
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
                messageResult.Success = false;
                messageResult.Message = ex.Message;
            }
            return messageResult;
        }
    }
}
