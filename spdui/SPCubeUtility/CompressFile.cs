using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;

namespace SPCubeUtility
{
    public class Compress
    {
        private const string COMPRESS_ONE_FILE = "a -o+ -ep {0} {1}";
        private const string COMPRESS_MULTI_FILE = "a -o+ -ep -vd -v{2} {0} {1}";        
        private const string DECOMPRESS_FILE = "e -o+ {0} {1}";
        private const int TIME_OUT_MINUTES = 60;
        public static void CompressFile(string compressProgram, string sourceFile, string targetFile)
        {
            CompressFile(compressProgram, new string[] { sourceFile }, targetFile, 0, TIME_OUT_MINUTES);
        }

        public static void CompressFile(string compressProgram, string[] sourceFile, string targetFile)
        {
            CompressFile(compressProgram, sourceFile, targetFile, 0, TIME_OUT_MINUTES);
        }

        public static string CompressFile(string compressProgram, string sourceFile, string targetFile, int sizeM)
        {
            return CompressFile(compressProgram, new string[] { sourceFile }, targetFile, sizeM, TIME_OUT_MINUTES);
        }

        public static string CompressFile(string compressProgram, string[] sourceFiles, string targetFile, int sizeM, int compressTimeoutMinutes)
        {
            string result = targetFile;
            File.Delete(targetFile);
            Process p = new Process();
            p.StartInfo.FileName = compressProgram;

            if (sizeM == 0)
            {
                p.StartInfo.Arguments = string.Format(COMPRESS_ONE_FILE, "\"" + targetFile + "\"",
                    GetSourceFileString(sourceFiles));
            }
            else
            {
                p.StartInfo.Arguments = string.Format(COMPRESS_MULTI_FILE, "\"" + targetFile + "\"",
                    GetSourceFileString(sourceFiles), Convert.ToString(sizeM * 1000));
            }
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            
            p.EnableRaisingEvents = true;
            
            //p.PriorityClass = ProcessPriorityClass.High;
            DateTime timeoutDati = DateTime.Now.AddMinutes(Convert.ToDouble(compressTimeoutMinutes));
            p.Start();
            
            while (!p.HasExited)
            {
                if (DateTime.Now > timeoutDati)
                {
                    throw (new Exception("Compress File Timeout!"));
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            p.Dispose();

            if (!File.Exists(result))
            {
                result = result.Insert(result.LastIndexOf('.'), ".part01");
            }
            return result;
        }

        private static string GetSourceFileString(string[] sourceFiles)
        {
            string result = "";
            foreach (string sourceFile in sourceFiles)
            {
                if (sourceFile.Trim() != "")
                {
                    result += "\"" + sourceFile.TrimEnd('\\') + "\" ";
                }
            }
            return result;
        }

        public static void DecompressFile(string compressProgram, string sourceFile, string targetFolder)
        {
            //File.Delete(targetFolder);
            Process p = new Process();
            p.StartInfo.FileName = compressProgram;
            p.StartInfo.Arguments = string.Format(DECOMPRESS_FILE, sourceFile, targetFolder);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.EnableRaisingEvents = true;            
            p.Start();

            while (!p.HasExited)
            {
                Thread.Sleep(500);
            }

        }

        public static string[] GetAllCompressParts(string firstPart)
        {
            ArrayList al = new ArrayList();
            int i = 1;
            int partIndex = firstPart.IndexOf("part01.rar");
            if (partIndex == -1)
            {
                al.Add(firstPart);
            }
            else
            {
                string prefix = firstPart.Substring(0, partIndex);
                string currentPart = firstPart;
                while (File.Exists(currentPart))
                {
                    al.Add(currentPart);
                    i++;
                    currentPart = prefix + "part" + i.ToString("00") + ".rar";
                }
            }

            return (string[])al.ToArray(typeof(string));
        }
    }
}
