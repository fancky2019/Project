using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZDFixService.Utility
{
    public class TxtFile
    {
        public static string ReadString(string filePath)
        {
            string jsonStr = "";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
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
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open)))
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
            }
            return content;
        }

        private static void CheckDirectory(string filePath )
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




        public static void SaveString(string filePath, string content)
        {
            CheckDirectory(filePath);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                fs.Write(data, 0, data.Length);
            }
        }

    }
}
