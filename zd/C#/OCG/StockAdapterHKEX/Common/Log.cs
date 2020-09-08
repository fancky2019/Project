using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAdapterHKEX.Common
{

    public class LogManager
    {
        static Dictionary<string, Log> _logs = null;
        static SpinLock _spinLock;
        static LogManager()
        {
            _logs = new Dictionary<string, Log>();
            _spinLock = new SpinLock(false);
            AppDomain.CurrentDomain.ProcessExit += (o, e) => { ShutDown(); };
        }

        /// <summary>
        /// 文件名，不包括路径
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public static Log GetLogger(string logName)
        {
            if (logName.Contains("\\") || logName.Contains("/")|| logName.Contains("."))
            {
                throw new Exception("Invalid logName,logName shold not contain path or extension .");
            }
            bool lockToken = false;
            _spinLock.Enter(ref lockToken);
            Log logger = null;
            if (!_logs.TryGetValue(logName, out logger))
            {
                logger = new Log(logName);
                _logs.Add(logName, logger);
            }

            _spinLock.Exit();
            return logger;
        }

        static void ShutDown()
        {
            foreach (var log in _logs.Values)
            {
                log.Dispose();
            }
        }
    }
    /// <summary>
    /// 一天生成一个文件
    /// 测试发现：10个文件没有写入延迟，文件超过20个写会有1ms写入延迟。
    /// </summary>
    public class Log
    {
        private StreamWriter _sw = null;
        private DateTime _createLogTime;
        private SpinLock _spinLock;
        private string _logName;
        private string _filePath;
        public Log(string logName)
        {
            _spinLock = new SpinLock(false);
            _createLogTime = DateTime.Now;
            this._logName = logName;
            CreateLogFile();
        }

        private void CreateLogFile()
        {
            if (_sw != null)
            {
                _sw.Dispose();
                _sw = null;
            }
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Log\\{DateTime.Now.Year}-{DateTime.Now.Month.ToString("D2")}\\{DateTime.Now.Year}-{DateTime.Now.Month.ToString("D2")}-{DateTime.Now.Day.ToString("D2")}\\{_logName}.log");
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            _sw = new StreamWriter(_filePath, true, System.Text.Encoding.UTF8);
        }


        public void WriteLog(string content)
        {
            bool lockToken = false;
            _spinLock.Enter(ref lockToken);
            if (_sw == null)
            {
                throw new Exception("File is disposed!");
            }
            if (DateTime.Now.Date != _createLogTime.Date)
            {
                CreateLogFile();
            }
            _sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {content}");
            //_sw.Flush();
            _spinLock.Exit();
        }

        public void Dispose()
        {
            bool lockToken = false;
            _spinLock.Enter(ref lockToken);
            _sw.Flush();
            _sw.Dispose();
            _sw = null;
            _spinLock.Exit();
        }

        //public void Test()
        //{
        //    //var reateDate = DateTime.Now.Day;
        //    //FileStream fileStream = _sw.BaseStream as FileStream;
        //    //var fileName = fileStream.Name;
        //    //Dispose();
        //    //return;
        //    //Task.Run(() =>
        //    //{
        //    //    Random random = new Random();

        //    //    while (true)
        //    //    {
        //    //        string logStr = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.fff")} Form1 Test";
        //    //        WriteLog(logStr);
        //    //        Thread.Sleep(random.Next(1, 2000));
        //    //    }
        //    //});
        //}
    }
}
