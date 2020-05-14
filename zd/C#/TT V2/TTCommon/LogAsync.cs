using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TT.Common;

namespace TTMarketAdapter.Utilities
{

    public class LogAsync
    {

        static BlockingCollection<string[]> _blockingCollection = null;
        static ConcurrentQueue<List<string>> _concurrentQueue = null;

        static ConcurrentDictionary<string, ConcurrentQueue<string>> _contractLogs = null;

        static ConcurrentQueue<string> _buffer = null;

        /// <summary>
        /// 定时器当前操作的合约
        /// </summary>
        static volatile string _timerContract = "";
        /// <summary>
        /// 达到cacheSize操作的合约
        /// </summary>
        static volatile string _cacheSizeContract = "";

        /// <summary>
        /// 缓存20条在一次刷磁盘
        /// </summary>
        public static int CacheSize { get; set; }

        /// <summary>
        /// 保存不活跃的合约：不活跃条数达不到缓存大小不能保存的情况
        /// </summary>
        static Timer _timer = null;

        /// <summary>
        /// 保存不活跃的合约：不活跃条数达不到缓存大小不能保存的情况 单位分钟
        /// </summary>
        public static int TimerInterval { get; set; }
        //public static bool LogInBatch = true;
        static LogAsync()
        {
            CacheSize = 20;
            TimerInterval = 10;
            _blockingCollection = new BlockingCollection<string[]>();
            //_concurrentQueue = new ConcurrentQueue<List<string>>();
            _contractLogs = new ConcurrentDictionary<string, ConcurrentQueue<string>>();
            _buffer = new ConcurrentQueue<string>();

            //if (!string.IsNullOrEmpty(Configurations.TimerInterval))
            //{
            //    _timerInterval = int.Parse(Configurations.TimerInterval) * 60 * 1000;
            //}

            _timer = new Timer(p =>
            {
                Poll();
            }, null, TimerInterval * 60 * 1000, TimerInterval * 60 * 1000);

            //if (!string.IsNullOrEmpty(Configurations.LogCacheSize))
            //{
            //    _cacheSize = int.Parse(Configurations.LogCacheSize);
            //}
            Task.Run(() =>
            {
                Consume();
            });

        }



        static void Poll()
        {
            //var strFromApp = $"Poll {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
            //BufferLog.LogAsync(strFromApp);
            foreach (var keyValuePair in _contractLogs)
            {
                if (!keyValuePair.Value.IsEmpty)
                {
                    //没有被Consume线程写
                    if (_cacheSizeContract != keyValuePair.Key)
                    {
                        _timerContract = keyValuePair.Key;
                        var arr = keyValuePair.Key.Split(',');
                        LogInBatch(arr[0], arr[1], keyValuePair.Value);
                    }
                    else
                    {
                        //如果该合约被Consume线程写，跳过。
                        continue;
                    }

                }
            }
        }

        static void Consume()
        {
            while (true)
            {
                foreach (var content in _blockingCollection.GetConsumingEnumerable())
                {

                    //  Log(content[0], content[1], content[2]);

                    var key = $"{content[0]},{content[1]}";
                    if (!_contractLogs.ContainsKey(key))
                    {
                        var val = new ConcurrentQueue<string>();
                        val.Enqueue(content[2]);
                        _contractLogs.TryAdd(key, val);
                    }
                    else
                    {
                        _contractLogs[key].Enqueue(content[2]);
                    }

                    //达到缓冲大小，写入磁盘
                    if (_contractLogs[key].Count == CacheSize)
                    {
                        //没有被定时器线程占用
                        if (_timerContract != key)
                        {
                            _cacheSizeContract = key;
                            LogInBatch(content[0], content[1], _contractLogs[key]);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }


                //if( _concurrentQueue.TryDequeue(out List<string> content))
                // {
                //     Log(content[0], content[1], content[2]);
                // }
                //else
                // {
                //     SpinWait spinWait = default(SpinWait);
                //     spinWait.SpinOnce();

                //     //Thread.Sleep(1);
                // }
            }
        }






        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="zdProduct">BRN</param>
        ///// <param name="zdCode">BRN1912</param>
        ///// <param name="sendMsg">@字符串</param>
        //static void Log(string zdProduct, string zdCode, string sendMsg)
        //{
        //    //格式为：1811
        //    string replaceZDCodeStr = zdCode.Replace(zdProduct, "");
        //    //if(replaceZDCodeStr.Length>4)//说明是期权，格式：1811 34
        //    //{
        //    //    string[] replaceZDCodeStrArray = replaceZDCodeStr.Split(' ');
        //    //    string excutePrice = replaceZDCodeStrArray[1];//获取执行价。
        //    //    if(excutePrice.Contains("."))//如果是小数
        //    //    {
        //    //        while (excutePrice.EndsWith("0"))
        //    //        {
        //    //            excutePrice = excutePrice.TrimEnd('0');
        //    //        }
        //    //        excutePrice = excutePrice.TrimEnd('.');
        //    //    }
        //    //    replaceZDCodeStr = replaceZDCodeStrArray[0]+" "+excutePrice;
        //    //}
        //    string dic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        //     $@"SendMsg\{zdProduct}\{replaceZDCodeStr}\{DateTime.Now.Year}\{DateTime.Now.Month}");
        //    //$@"SendMsg\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}\{zdProduct}\{zdCode.Replace(zdProduct,"")}");
        //    if (!Directory.Exists(dic))
        //    {
        //        Directory.CreateDirectory(dic);
        //    }
        //    string fileName = Path.Combine(dic, $"{DateTime.Now.Day}.txt");

        //    using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8))
        //    {
        //        sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}  {sendMsg}");
        //        //sw.WriteAsync($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}  {sendMsg}\r\n");
        //    }


        //}


        static void LogInBatch(string zdProduct, string zdCode, ConcurrentQueue<string> sendMsgs)
        {
            //格式为：1811
            string replaceZDCodeStr = zdCode.Replace(zdProduct, "");
            //if(replaceZDCodeStr.Length>4)//说明是期权，格式：1811 34
            //{
            //    string[] replaceZDCodeStrArray = replaceZDCodeStr.Split(' ');
            //    string excutePrice = replaceZDCodeStrArray[1];//获取执行价。
            //    if(excutePrice.Contains("."))//如果是小数
            //    {
            //        while (excutePrice.EndsWith("0"))
            //        {
            //            excutePrice = excutePrice.TrimEnd('0');
            //        }
            //        excutePrice = excutePrice.TrimEnd('.');
            //    }
            //    replaceZDCodeStr = replaceZDCodeStrArray[0]+" "+excutePrice;
            //}
            string dic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
             $@"SendMsg\{zdProduct}\{replaceZDCodeStr}\{DateTime.Now.Year}\{DateTime.Now.Month}");
            //$@"SendMsg\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}\{zdProduct}\{zdCode.Replace(zdProduct,"")}");
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            string fileName = Path.Combine(dic, $"{DateTime.Now.Day}.txt");

            using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8))
            {
                string timeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                while (!sendMsgs.IsEmpty)
                {
                    string sendMsg = "";
                    sendMsgs.TryDequeue(out sendMsg);
                    sw.WriteLine($"{timeStr}  {sendMsg}");
                }


                //sw.WriteAsync($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}  {sendMsg}\r\n");
            }


        }

        public static void Log(params string[] message)
        {
            if (!_blockingCollection.IsAddingCompleted)
            {
                _blockingCollection.Add(message);

            }

            //_concurrentQueue.Enqueue(message);
        }

        public static void Close()
        {
            _blockingCollection.CompleteAdding();
        }


    }
}
