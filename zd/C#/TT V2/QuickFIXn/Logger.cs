using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QuickFix
{
    public abstract class FixZDLogger : IDisposable
    {
        public const int LVL_DEBUG = 4;
        public const int LVL_TRACE = 3;
        public const int LVL_CRITCAL = 2;
        public const int LVL_ERROR = 1;

        public abstract void log(int logLvl, string content);
        public abstract void log(int logLvl, string classN, string method, string content);
        public abstract void setLogLevel(int logLvl);
        public abstract void Dispose();
    }

    public class ZDLoggerFactory
    {
        private static FixZDLogger synWriteLogger = null;
        //private static ZDLogger asyWriteLogger = null;
        private static object myLock = new object();

        public static FixZDLogger getSynWriteLogger(string logFile)
        {
            lock (myLock)
            {
                if (synWriteLogger == null)
                {
                    synWriteLogger = new SynWriteLogger(logFile);
                    synWriteLogger.setLogLevel(FixZDLogger.LVL_DEBUG);

                }

                return synWriteLogger;
            }
        }

        public static FixZDLogger getSynWriteLogger(string logPath, string logFile)
        {
            lock (myLock)
            {
                if (synWriteLogger == null)
                {
                    synWriteLogger = new SynWriteLogger(logPath, logFile);
                    synWriteLogger.setLogLevel(FixZDLogger.LVL_DEBUG);
                }

                return synWriteLogger;
            }
        }
    }

    public class SynWriteLogger : FixZDLogger
    {
        private bool _disposed;

        private int writeLvl = 0;
        private string logPath = null;
        private StreamWriter streamWriter = null;

        private string fileName = null;

        public bool isInitFailed = false;

        public SynWriteLogger(string fileName)
        {
            //this.fileName = fileName;

            //string date = "_" + DateTime.Now.ToString("yyyyMMdd") + ".";
            //fileName = fileName.Replace(".", date);
            //streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write));

            logPath = "log";
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            string strDate = DateTime.Now.ToString("yyyyMMdd");
            logPath = logPath + @"\" + strDate;
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            this.fileName = fileName;

            string date = "_" + strDate + ".";
            fileName = logPath + @"\" + fileName.Replace(".", date);

            /*
            if (File.Exists(fileName))
            {
                isInitFailed = true;
            }
            else
             */
            streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write));
        }

        public SynWriteLogger(string path, string fileName)
        {
            logPath = path;
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            string strDate = DateTime.Now.ToString("yyyyMMdd");
            logPath = logPath + @"\" + strDate;
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            this.fileName = fileName;

            string date = "_" + strDate + ".";
            fileName = logPath + @"\" + fileName.Replace(".", date);
            streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write));
        }

        public override void setLogLevel(int logLvl)
        {
            this.writeLvl = logLvl;
        }


        public override void log(int logLvl, string content)
        {
            try
            {
                if (logLvl > writeLvl) return;

                //Console.WriteLine(content);
                string time = DateTime.Now.ToString("yyyyMMdd HH:mm:ss:fff");
                lock (streamWriter)
                {
                    streamWriter.Write("{0} [{1}] - ", time, logLvl);
                    streamWriter.WriteLine(content);
                    streamWriter.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void log(int logLvl, string classN, string method, string content)
        {
            try
            {
                if (logLvl > writeLvl) return;

                //Console.WriteLine(content);
                string time = DateTime.Now.ToString("yyyyMMdd HH:mm:ss:fff");
                lock (streamWriter)
                {
                    streamWriter.Write("{0} [{1}] - {2}#{3}() ", time, logLvl, classN, method);
                    streamWriter.WriteLine(content);
                    streamWriter.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                //if (disposing)
                //{
                //    // Dispose managed resources.
                //}

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                if (streamWriter != null)
                    streamWriter.Dispose();

                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }

    }

    //public class AsyWriteLogger : ZDLogger
    //{
    //    public void log(int logLvl, string content)
    //    {
    //    }
    //}
}
