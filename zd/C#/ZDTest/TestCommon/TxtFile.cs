using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestCommon
{
    public class TxtFile
    {
        public static string ReadString(string filePath)
        {
            string jsonStr = "";
            if (!File.Exists(filePath))
            {
                return jsonStr;
            }

            //using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                var bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                jsonStr = Encoding.UTF8.GetString(bytes);
            }

            return jsonStr;
        }

        public static List<string> ReadTxtFile(string filePath)
        {
            List<string> content = new List<string>();
            if (!File.Exists(filePath))
            {
                return content;
            }
            //_sw = new StreamWriter(File.Open(_filePath, FileMode.Append, FileAccess.Write), System.Text.Encoding.UTF8);
            //using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open)))
            // FileStream fs = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                try
                {
                    while (!sr.EndOfStream)
                    {
                        // string line = sr.ReadLine().Trim();
                        content.Add(sr.ReadLine().Trim());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return content;
        }

        private static void CheckDirectory(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void SaveTxtFile(string filePath, List<string> content, FileMode fileMode = FileMode.Create)
        {
            CheckDirectory(filePath);
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, fileMode, FileAccess.ReadWrite), System.Text.Encoding.UTF8))
            {
                foreach (string str in content)
                {
                    sw.WriteLine(str);
                }
            }
        }

        public static void SaveTxtFile(string filePath, string content, FileMode fileMode = FileMode.Create)
        {
            CheckDirectory(filePath);
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, fileMode, FileAccess.ReadWrite), System.Text.Encoding.UTF8))
            {
                sw.WriteLine(content);
            }
        }



        public static void SaveString(string filePath, string content, FileMode fileMode = FileMode.Create)
        {
            CheckDirectory(filePath);
            using (FileStream fs = new FileStream(filePath, fileMode))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                fs.Write(data, 0, data.Length);
            }
        }

        public static void BackUpSaveString(string filePath, string content, FileMode fileMode = FileMode.Create)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            string destFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_backup{Path.GetExtension(filePath)}";
            var destFullFileName = Path.Combine(Path.GetDirectoryName(filePath), destFileName);
            File.Copy(filePath, destFullFileName, true);
            SaveString(filePath, content);
            File.Delete(destFullFileName);
        }

        public static void BackUp(string sourceFileName)
        {
            if (!File.Exists(sourceFileName))
            {
                return;
            }
            string destFileName = $"{Path.GetFileNameWithoutExtension(sourceFileName)}_backup{Path.GetExtension(sourceFileName)}";
            File.Copy(sourceFileName, destFileName, true);
        }




        public static void AppendFile(string filePath, string content)
        {
            CheckDirectory(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(content);
                sw.Flush();
            }
        }

    }
}
