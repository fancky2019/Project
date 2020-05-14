using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TT.Common
{
    public class Zip
    {
        public static void ZipLog()
        {
            try
            {
                //string sendMsgRootPath = @"D:\ZProject\connamara-quickfixn-2d35bed-TT\TTMarketAdapter\bin\Debug\SendMsg";
                string sendMsgRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"SendMsg");
                //获取所有的.txt文件，文件夹含有压缩的.zip文件
                List<string> fileList = Directory.GetFiles(sendMsgRootPath, "*.txt", SearchOption.AllDirectories).ToList();

                //TT.Common.Log.Info<Zip>($"fileList:{JsonHelper.JsonSerializeObjectFormat(fileList)}.");
                foreach (var p in fileList)
                {
                    string fileName = Path.GetFileNameWithoutExtension(p);
                    if (int.TryParse(fileName, out int day))//防止文件名称不是数字
                    {
                        //不压缩当天的日记文件
                        if (day == DateTime.Now.Day)
                        {
                            continue;
                        }
                        ZipFile(p);
                    }
                    else//文件名称不是数字的直接压缩
                    {
                        ZipFile(p);
                    }

                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.Message);
            }
        }

        //压缩
        static void ZipFile(string fileFullPath)
        {
            if (!File.Exists(fileFullPath))
            {
                return;
            }
            string zipFilePath = fileFullPath.Replace(Path.GetExtension(fileFullPath), ".zip");
            try
            {
                //TT.Common.Log.Info<Zip>($"Begin zip file:{zipFilePath}.");
                //安装压缩程序包 SharpZipLib 程序包
                //string[] filenames = Directory.GetFiles(path);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    // 压缩级别 0-9
                    s.SetLevel(9);
                    //s.Password = "123"; //Zip压缩文件密码
                    byte[] buffer = new byte[4096]; //缓冲区大小
                    ZipEntry entry = new ZipEntry(Path.GetFileName(fileFullPath));
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);
                    using (FileStream fs = File.OpenRead(fileFullPath))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                    s.Finish();
                    s.Close();
                }
                //压缩成功后删除.txt文件
                File.Delete(fileFullPath);
                //TT.Common.Log.Info<Zip>($"End zip file:{zipFilePath} complete.");
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(zipFilePath);
                TT.Common.NLogUtility.Error(ex.ToString());
                FileUsing.FileIsUsing(zipFilePath);
            }

        }

        public static void KeepingThirtyDaysLogs()
        {
            string sendMsgRootPath = Path.Combine(Directory.GetCurrentDirectory(), $@"SendMsg");
            //获取所有的.txt文件，文件夹含有压缩的.zip文件
            List<string> fileList = Directory.GetFiles(sendMsgRootPath, "*.*", SearchOption.AllDirectories).ToList();
            foreach (var p in fileList)
            {
                try
                {
                    //D:\ZProject\TTOld\MD\connamara-quickfixn-2d35bed-TT\TTMarketAdapter\bin\Debug\SendMsg\JB\1903\2018\3\23.txt
                    var folderFileNames = p.Split('\\');
                    var dayofMonth = folderFileNames[folderFileNames.Length - 1].Split('.')[0];
                    var month = folderFileNames[folderFileNames.Length - 2];
                    var year = folderFileNames[folderFileNames.Length - 3];
                    string pattern = "[0-9]{1,4}";
                    if (!Regex.IsMatch(dayofMonth, pattern) || !Regex.IsMatch(month, pattern) || !Regex.IsMatch(year, pattern))
                    {
                        continue;
                    }
                    DateTime logDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(dayofMonth));
                    DateTime currentDate = DateTime.Now.Date;
                    TimeSpan timeSpan = currentDate - logDate;
                    if (timeSpan.Days > 30)
                    {
                        File.Delete(p);
                        var parentDirectory = Directory.GetParent(p);
                    }
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                    continue;
                }
            }
            DeleteEmptyFolder();
        }

        private static void DeleteEmptyFolder()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), $@"SendMsg"));
                DirectoryInfo[] subDirectoryInfos = dir.GetDirectories("*", SearchOption.AllDirectories);
                foreach (DirectoryInfo di in subDirectoryInfos)
                {
                    FileSystemInfo[] subFiles = di.GetFileSystemInfos();
                    if (subFiles.Count() == 0)
                    {
                        di.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }

        }
    }
}
