using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AnyOrigin")]
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
        [HttpGet("GetClientIpTest")]
        public string GetClientIpTest()
        {
            //使用nginx代理后无法获取客户端地址。
            //   HttpContext.HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
            //var obj = this.HttpContext.Request;
            //MVC里获取
            //this.Request.UserHostAddress
            //string IpA = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //string IpB = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //.NET CORE 获取ip地址
            string ipaddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            return ipaddress;
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

                List<string> imgExtensions = new List<string>() { "jpg", "jpeg", "gif", "bmp", "png" };
                //foreach (var file in files)
                //{
                //}
                var extentsions = files.Select(p => Path.GetExtension(p.FileName).ToLower()).ToList();
                if (!extentsions.Exists(p => !imgExtensions.Contains(p)))
                {
                    messageResult.Success = false;
                    messageResult.Message = "附件存在非图片文件。";
                    return messageResult;
                }
                var param = this.HttpContext.Request.Form.Keys.ToList();

                var suggestion = this.HttpContext.Request.Form[param[0]];
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
                //跨域问题：返回数据跨域
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
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
