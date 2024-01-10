using CandidateCodeTest.Common.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace CandidateCodeTest.Common
{
    public class LogEntry : ILogEntry
    {
        private string logEntryPath = string.Empty;
        public LogEntry(string logMessage)
        {
            AddLogs(logMessage);
        }

        public void AddLogs(string logMessage)
        {
            logEntryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using StreamWriter w = File.AppendText(logEntryPath + "\\" + "log.txt");
                Log(logMessage, w);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This is used to write the log in the current assembly
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="txtWriter"></param>
        private static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
