using System;
using System.Diagnostics;
using System.IO;

namespace Common.Helpers
{
    public class Logs
    {
        public static void LogFile(string sLog, string path = null)
        {
            try
            {
                path = path ?? Setting.LOG_FILE;
                MemoryStream stream = new MemoryStream();
                byte[] buffer = File.ReadAllBytes(path);
                byte[] newData = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString() + "--" + sLog + Environment.NewLine);
                stream.Write(newData, 0, newData.Length);
                stream.Write(buffer, 0, buffer.Length);
                File.WriteAllBytes(path, stream.GetBuffer());
                stream.Dispose();
            }
            catch (Exception)
            {}
        }
    }
}
