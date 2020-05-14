
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace QuickFix
{
    /// <summary>
    /// File log implementation
    /// </summary>
    public class FileLog : ILog, System.IDisposable
    {
        private object sync_ = new object();

        private System.IO.StreamWriter messageLog_;
        private System.IO.StreamWriter eventLog_;

        private string messageLogFileName_;
        private string eventLogFileName_;

        private bool _disposed = false;

        private void NewLogFile()
        {

            System.Threading.Timer timer = new System.Threading.Timer((param) =>
            {
                //string logDateStr = messageLogFileName_.Substring(messageLogFileName_.Length - 27, 10);
                //string nowDateStr = DateTime.Now.ToString("yyyy-MM-dd");

                string logDateMinStr = messageLogFileName_.Substring(messageLogFileName_.Length - 33, 16);

                string todayMinStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm");
                //if (logDateStr != nowDateStr)
                if (logDateMinStr != todayMinStr)
                {
                    DisposedCheck();

                    lock (sync_)
                    {
                        messageLog_.Close();
                        eventLog_.Close();

                        //messageLogFileName_ = messageLogFileName_.Replace(logDateStr, nowDateStr);
                        //eventLogFileName_= eventLogFileName_.Replace(logDateStr, nowDateStr);


                        messageLogFileName_ = messageLogFileName_.Replace(logDateMinStr, todayMinStr);
                        eventLogFileName_ = eventLogFileName_.Replace(logDateMinStr, todayMinStr);


                        messageLog_ = new System.IO.StreamWriter(messageLogFileName_, true);
                        eventLog_ = new System.IO.StreamWriter(eventLogFileName_, true);
                        messageLog_.AutoFlush = true;
                        eventLog_.AutoFlush = true;
                    }
                }
            }, null, 60*1000, 1  * 60 * 1000);
            //}, null, 2 * 60 * 1000, 1 * 60 * 60 * 1000);
        }
        private void NewLogFileWhile()
        {

            while (true)
            {
                Thread.Sleep(60 * 1000);
                //string logDateStr = messageLogFileName_.Substring(messageLogFileName_.Length - 27, 10);
                //string nowDateStr = DateTime.Now.ToString("yyyy-MM-dd");
                //                                            8         7      3
                //FIX.4.2-ZDDEV_SD-TT_PRICE.2019-08-02 16-59.messages.current.log
                string fileName =   Path.GetFileName(messageLogFileName_);
                //string logDateMinStr = fileName.Substring(fileName.Length - 37, 16);

                //string todayMinStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm");

                string logDateStr = fileName.Substring(fileName.Length - 31, 10);

                string nowDateStr = DateTime.Now.ToString("yyyy-MM-dd");
                //if (logDateStr != nowDateStr)
                if (logDateStr != nowDateStr)
                {
                    DisposedCheck();

                    lock (sync_)
                    {
                        messageLog_.Close();
                        eventLog_.Close();

                        //messageLogFileName_ = messageLogFileName_.Replace(logDateStr, nowDateStr);
                        //eventLogFileName_= eventLogFileName_.Replace(logDateStr, nowDateStr);


                        messageLogFileName_ = messageLogFileName_.Replace(logDateStr, nowDateStr);
                        eventLogFileName_ = eventLogFileName_.Replace(logDateStr, nowDateStr);


                        messageLog_ = new System.IO.StreamWriter(messageLogFileName_, true);
                        eventLog_ = new System.IO.StreamWriter(eventLogFileName_, true);
                        messageLog_.AutoFlush = true;
                        eventLog_.AutoFlush = true;
                    }
                }
            }
   
            //}, null, 2 * 60 * 1000, 1 * 60 * 60 * 1000);
        }

        public FileLog(string fileLogPath)
        {
            Init(fileLogPath, "GLOBAL");
        }

        private static bool _runned = false;
        public FileLog(string fileLogPath, SessionID sessionID)
        {
            Init(fileLogPath, Prefix(sessionID));
            if (!_runned)
            {
                Task.Factory.StartNew(() =>
                {
                    NewLogFileWhile();
                });
                //NewLogFile();
                _runned = true;
            }
        }


        private void Init(string fileLogPath, string prefix)
        {
            if (!System.IO.Directory.Exists(fileLogPath))
                System.IO.Directory.CreateDirectory(fileLogPath);
            string todayStr = DateTime.Now.ToString("yyyy-MM-dd");
            //string todayMinStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm");

            //messageLogFileName_ = System.IO.Path.Combine(fileLogPath, prefix +".messages.current.log");
            //eventLogFileName_ = System.IO.Path.Combine(fileLogPath, prefix + ".event.current.log");

            //messageLogFileName_ = System.IO.Path.Combine(fileLogPath, prefix + "." + todayStr + ".messages.current.log");
            //eventLogFileName_ = System.IO.Path.Combine(fileLogPath, prefix + "." + todayStr + ".event.current.log");

            messageLogFileName_ = System.IO.Path.Combine(fileLogPath, prefix + "." + todayStr + ".messages.current.log");
            eventLogFileName_ = System.IO.Path.Combine(fileLogPath, prefix + "." + todayStr + ".event.current.log");

            messageLog_ = new System.IO.StreamWriter(messageLogFileName_, true);
            eventLog_ = new System.IO.StreamWriter(eventLogFileName_, true);

            messageLog_.AutoFlush = true;
            eventLog_.AutoFlush = true;
        }

        public static string Prefix(SessionID sessionID)
        {
            System.Text.StringBuilder prefix = new System.Text.StringBuilder(sessionID.BeginString)
                .Append('-').Append(sessionID.SenderCompID);
            if (SessionID.IsSet(sessionID.SenderSubID))
                prefix.Append('_').Append(sessionID.SenderSubID);
            if (SessionID.IsSet(sessionID.SenderLocationID))
                prefix.Append('_').Append(sessionID.SenderLocationID);
            prefix.Append('-').Append(sessionID.TargetCompID);
            if (SessionID.IsSet(sessionID.TargetSubID))
                prefix.Append('_').Append(sessionID.TargetSubID);
            if (SessionID.IsSet(sessionID.TargetLocationID))
                prefix.Append('_').Append(sessionID.TargetLocationID);

            if (sessionID.SessionQualifier.Length != 0)
                prefix.Append('-').Append(sessionID.SessionQualifier);

            return prefix.ToString();
        }

        private void DisposedCheck()
        {
            if (_disposed)
                throw new System.ObjectDisposedException(this.GetType().Name);
        }

        #region Log Members

        public void Clear()
        {
            DisposedCheck();

            lock (sync_)
            {
                messageLog_.Close();
                eventLog_.Close();


                messageLog_ = new System.IO.StreamWriter(messageLogFileName_, false);
                eventLog_ = new System.IO.StreamWriter(eventLogFileName_, false);

                messageLog_.AutoFlush = true;
                eventLog_.AutoFlush = true;
            }
        }

        public void OnIncoming(string msg)
        {
            WriteToFile(msg);
        }

        public void OnOutgoing(string msg)
        {
            WriteToFile(msg);
        }

        private void WriteToFile(string msg)
        {
            DisposedCheck();

            lock (sync_)
            {
                messageLog_.WriteLine(Fields.Converters.DateTimeConverter.Convert(System.DateTime.UtcNow) + " : " + msg);
            }
        }



        public void OnEvent(string s)
        {
            DisposedCheck();

            lock (sync_)
            {
                eventLog_.WriteLine(Fields.Converters.DateTimeConverter.Convert(System.DateTime.UtcNow) + " : " + s);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (messageLog_ != null) { messageLog_.Dispose(); }
            if (eventLog_ != null) { eventLog_.Dispose(); }

            messageLog_ = null;
            eventLog_ = null;

            _disposed = true;
        }

        #endregion
    }
}
