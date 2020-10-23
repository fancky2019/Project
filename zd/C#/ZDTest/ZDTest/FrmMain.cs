using Model.Entity;
using Model.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestCommon;
using TestService.Netty;
using TestService.Services;
using ZDTest.UserControls;

namespace ZDTest
{
    public partial class FrmMain : Form
    {
        private static readonly Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        TClientBaseInfoService _clientBaseInfoService = new TClientBaseInfoService();
        List<TClientBaseInfo> _users = new List<TClientBaseInfo>();
        public FrmMain()
        {
            InitializeComponent();


            _users = _clientBaseInfoService.GetTClientBaseInfos();
        }


        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //var str = Configurations.Configuration["TestDal:PressTestConStr"];
            //_users = _clientBaseInfoService.GetTClientBaseInfos();
            var users = _users.Take((int)this.nudUserAccount.Value).ToList();

            //List<Thread> listThread = new List<Thread>();
            //users.ForEach(p =>
            //{
            //    //Task.Run(() =>
            //    //{

            //    Thread thread = new Thread(() =>
            //     {


            //         CommunicationClient client = new CommunicationClient(Configurations.Configuration["TestService:FrontIP"], Configurations.Configuration["TestService:FrontPort"]);
            //         client.ReceiveMsg += msg =>
            //           {
            //               _nLog.Info(msg);
            //           };
            //         client.Connected += () =>
            //           {
            //               var loginCommand = $"LOGINHK1@@ClientIP:172.17.254.65:PC_V2.0.392@C@@@192.168.2.166:59289@R@1@@@0&{p.FUpperNo}@888888@1@00:15:5D:64:81:32@DESKTOP-3HM9UQG";
            //               client.SendMsg<string>(loginCommand);
            //           };

            //         client.RunClientAsync();
            //         client.Connect();
            //     });
            //    thread.Name = p.FClientNo;
            //    //thread.Start();
            //    listThread.Add(thread);
            //    //});

            //});
            //listThread.ForEach(p =>
            //{
            //    p.Start();
            //});



            users.ForEach(p =>
            {
                Task.Run(() =>
                {
                    CommunicationClient client = new CommunicationClient(Configurations.Configuration["TestService:FrontIP"], Configurations.Configuration["TestService:FrontPort"]);
                    client.ReceiveMsg += msg =>
                      {
                          _nLog.Info(msg);
                      };
                    client.Connected += () =>
                      {
                          var loginCommand = $"LOGINHK1@@ClientIP:172.17.254.65:PC_V2.0.392@C@@@192.168.2.166:59289@R@1@@@0&{p.FUpperNo}@888888@1@00:15:5D:64:81:32@DESKTOP-3HM9UQG";
                          client.SendMsg<string>(loginCommand);
                      };
                    client.ClientNo = p.FClientNo;
                    client.RunClientAsync();
                    MemoryData.Users.TryAdd(p.FClientNo, new User() { ClientNo = p.FClientNo, ConnectingTime = DateTime.Now });
                    //client.Connect();
                });

            });


            #region  先创建
            //List<Task> tasks = new List<Task>();
            //users.ForEach(p =>
            //{
            //    Task task = new Task(() =>
            //      {



            //          CommunicationClient client = new CommunicationClient(Configurations.Configuration["TestService:FrontIP"], Configurations.Configuration["TestService:FrontPort"]);
            //          client.ReceiveMsg += msg =>
            //            {
            //                _nLog.Info(msg);
            //            };
            //          client.Connected += () =>
            //            {
            //                var loginCommand = $"LOGINHK1@@ClientIP:172.17.254.65:PC_V2.0.392@C@@@192.168.2.166:59289@R@1@@@0&{p.FUpperNo}@888888@1@00:15:5D:64:81:32@DESKTOP-3HM9UQG";
            //                client.SendMsg<string>(loginCommand);
            //            };

            //          client.RunClientAsync();
            //          client.Connect();
            //      });
            //    //task.Start();
            //    tasks.Add(task);
            //});

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //tasks.ForEach(t => t.Start());

            //Task.WhenAll(tasks);

            //stopwatch.Stop();
            //var mills = stopwatch.ElapsedMilliseconds;
            #endregion






            //Received from Communication:LOGINHK1@@ClientIP:172.17.254.65:PC_V2.0.392@C@00000@@192.168.2.166:59289@1000009888888120201021151611655@1@@@0&1000009@N1000009@C@888888@00000106@ @@N@1@HKD@U@0@0@0@0@0@0@0@0@0@0@@@@0@0@N@0@0@0@0@2@0@@@0@12345678901@1@0@1@0@@@@0@@@1@1^1000009@N1000009@C@888888@00000107@ @@N@1@RMB@U@0@0@0@0@0@0@0@0@0@0@@@@0@0@N@0@0@0@0@2@0@@@0@12345678901@1@0@1@0@@@@0@@@1@^1000009@N1000009@C@888888@00000108@ @@N@1@USD@U@0@0@0@0@0@0@0@0@0@0@@@@0@0@N@0@0@0@0@2@0@@@0@12345678901@1@0@1@0@@@@0@@@1@}{(len=88)LogHisLs@@@@00000@@@@1@@@0&192.168.1.105:60890@192.168.2.166:59289@2020-10-21 15:14:42@1|

            //var len = "LogHisLs@@@@00000@@@@1@@@0&192.168.1.105:60890@192.168.2.166:59289@2020-10-21 15:14:42@1";

            //int length = len.Length;
            //var logContent = @"{(len=481)LOGINHK1@@ClientIP:172.17.254.65:PC_V2.0.392@C@00000@@192.168.2.166:59289@1000009888888120201021154448048@1@@@0&1000009@N1000009@C@888888@00000106@ @@N@1@HKD@U@0@0@0@0@0@0@0@0@0@0@@@@0@0@N@0@0@0@0@2@0@@@0@12345678901@1@0@1@0@@@@0@@@1@1^1000009@N1000009@C@888888@00000107@ @@N@1@RMB@U@0@0@0@0@0@0@0@0@0@0@@@@0@0@N@0@0@0@0@2@0@@@0@12345678901@1@0@1@0@@@@0@@@1@^1000009@N1000009@C@888888@00000108@ @@N@1@USD@U@0@0@0@0@0@0@0@0@0@0@@@@0@0@N@0@0@0@0@2@0@@@0@12345678901@1@0@1@0@@@@0@@@1@}{(len=88)LogHisLs@@@@00000@@@@1@@@0&192.168.1.105:64458@192.168.2.166:59289@2020-10-21 15:39:25@1}";

            //var endString = "}";
            //int m = 0;
            //int firstIndex = logContent.IndexOf(endString);
            //var logContentLength = logContent.Length;
            //List<string> listString = new List<string>();
            //if (firstIndex == logContentLength - 1)
            //{
            //    //var content = RemoveLength(logContent);
            //    listString.Add(RemoveLength(logContent));
            //}
            //else
            //{
            //    string currentContent = "";
            //    while (firstIndex != logContentLength - 1)
            //    {
            //        currentContent = logContent.Substring(0, firstIndex + 1);
            //        //listString.Add(currentContent);


            //        var content = RemoveLength(currentContent);
            //        listString.Add(content);

            //        logContent = logContent.Substring(firstIndex + 1);
            //        logContentLength = logContent.Length;
            //        firstIndex = logContent.IndexOf(endString);
            //    }
            //    listString.Add(RemoveLength(logContent));
            //}


            //var data = _clientBaseInfoService.GetTClientBaseInfos();
            //var str = Configurations.Configuration["TestDal:PressTestConStr"];



        }

        private string GetLength(string logContent)
        {
            var lenIndexStr = "len=";
            var lenIndexStrIndex = logContent.IndexOf(lenIndexStr);
            var lenEndStr = ")";
            var lenEndStrIndex = logContent.IndexOf(lenEndStr);

            var lenNumIndex = lenIndexStrIndex + lenIndexStr.Length;
            var lengthStr = logContent.Substring(lenNumIndex, lenEndStrIndex - lenNumIndex);



            return "";
        }
        private string RemoveLength(string logContent)
        {

            var lenEndStr = ")";
            var lenEndStrIndex = logContent.IndexOf(lenEndStr);
            var content = logContent.Substring(lenEndStrIndex + 1, logContent.Length - (lenEndStrIndex + 1) - 1);
            return content;
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            //var loginCommand = $"LOGINHK1@@ClientIP:172.17.254.65:PC_V2.0.392@C@@@192.168.2.166:59289@R@1@@@0&1000009@888888@1@00:15:5D:64:81:32@DESKTOP-3HM9UQG";
            //_client.SendMsg<string>(loginCommand);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //_client.Close();
        }

        Login _login = null;
        private void btnLoginTest_Click(object sender, EventArgs e)
        {
            _login = new Login();
            _login.Dock = DockStyle.Fill;
            this.panelUserControl.Controls.Add(_login);
        }

        private void btnLoadMemoryDta_Click(object sender, EventArgs e)
        {
            _login.Users = MemoryData.Users.Values.ToList();
        }
    }
}
