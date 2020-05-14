using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TT.Common
{
    public  class FileUsing
    {
        public static void FileIsUsing(string fileName)
        {
            if(!File.Exists("handle64.exe"))
            {
                NLogUtility.Info("Can not find file \"handle64.exe\".");
                return;
            }
            Process tool = new Process();
            tool.StartInfo.FileName = "handle64.exe";
            //tool.StartInfo.Arguments = fileName + " /accepteula";
            tool.StartInfo.Arguments = fileName + " /accepteula";
            tool.StartInfo.UseShellExecute = false;
            tool.StartInfo.RedirectStandardOutput = true;
            tool.Start();
            tool.WaitForExit();

            // Demos.exe pid: 1604   type: File            48: C: \Users\Administrator\Desktop\test.txt
            string outputTool = tool.StandardOutput.ReadToEnd();
            NLogUtility.Info(outputTool);
            ////pid
            //string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
            //foreach (Match match in Regex.Matches(outputTool, matchPattern))
            //{
            //    var process = Process.GetProcessById(int.Parse(match.Value));
            //    //var processName = process.ProcessName;
            //    //process.Kill();
            //}

            var pattern = @"(\S+.exe)\b(?=\s+)\b(?=\s+)";
            var matches = Regex.Matches(outputTool, pattern);

            foreach (Match ma in matches)
            {
                var processName = ma.Value;
                NLogUtility.Info(processName);
            }


        }
    }
}
