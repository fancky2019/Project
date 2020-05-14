using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT.Common;

namespace Sentinel
{
    public partial class FrmSentinel : Form
    {
        private int _sentitelAssistantInterval = 10000;
        public FrmSentinel()
        {
            InitializeComponent();
            //_sentitelAssistantInterval = int.Parse(ConfigurationManager.AppSettings["SentitelAssistantInterval"]) * 1000;
            //  SentitelAssistant();
            RestartOnMonday();
        }

        private void SentitelAssistant()
        {

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int workingProcessCount = 0;
                    foreach (var prcessName in ConfigurationManager.AppSettings.AllKeys)
                    {
                        if (Process.GetProcessesByName(prcessName).Length > 0)
                        {
                            workingProcessCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (workingProcessCount == ConfigurationManager.AppSettings.AllKeys.Count() - 1)
                    {
                        Sentinel();
                    }
                    Thread.Sleep(_sentitelAssistantInterval);
                }

            });
        }

        /// <summary>
        /// 周一上午8点重启
        /// </summary>
        private void RestartOnMonday()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    {
                        if (DateTime.Now.Hour == 8)
                        {
                            Sentinel();
                            Log4NetUtility.Info<FrmSentinel>("Sentinel 已重启。");
                        }
                    }
                    Thread.Sleep(1 * 60 * 60 * 1000);
                }

            });
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Sentinel();
            this.btnStart.Text = "ReStart";
        }

        private void Sentinel()
        {
            List<Process> runningProcessList = new List<Process>();
            foreach (var prcessName in ConfigurationManager.AppSettings.AllKeys)
            {
                runningProcessList.AddRange(Process.GetProcessesByName(prcessName));
            }
            foreach (Process process in runningProcessList)
            {
                process.EnableRaisingEvents = true;//设置进程终止时触发Exited事件
                process.Exited += Process_Exited;
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            var exitProcess = sender as Process;
            try
            {
                Log4NetUtility.Info<FrmSentinel>($"{exitProcess.ProcessName}:关闭。");
                if (this.cbSentinel.Checked)
                {
                    Thread.Sleep(60 * 1000);
                    if (Process.GetProcessesByName(exitProcess.ProcessName).Length > 0)
                    {
                        Log4NetUtility.Info<FrmSentinel>($"{exitProcess.ProcessName}:外部已启动。");
                    }
                    else
                    {
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (exitProcess.ProcessName == "TTMarketAdapter")
                            {
                                Log4NetUtility.Info<FrmSentinel>($"{exitProcess.ProcessName}:周日不重启。");
                                return;
                            }
                        }
                        Process process = new Process();
                        process.StartInfo.FileName = ConfigurationManager.AppSettings[exitProcess.ProcessName];
                        //var reStartProcess = Process.Start(ConfigurationManager.AppSettings[exitProcess.ProcessName]);
                        process.Start();
                        Log4NetUtility.Info<FrmSentinel>($"{exitProcess.ProcessName}:恢复启动。");
                    }
                    Sentinel();
                }
            }
            catch (System.ComponentModel.Win32Exception exception)
            {
                Log4NetUtility.Error<FrmSentinel>($"{exitProcess.ProcessName}:{exception.Message}。");
            }
            catch (Exception ex)
            {
                Log4NetUtility.Error<FrmSentinel>($"{exitProcess.ProcessName}:{ex.ToString()}。");
            }
        }

        private void BtnDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
