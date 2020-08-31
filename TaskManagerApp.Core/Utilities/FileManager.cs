using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaskManagerApp.Core.Utilities
{
    public class FileManager
    {
        public async static void WriteAsync(string path,string value)
        {
            using StreamWriter streamWriter = new StreamWriter(path, true);
            await streamWriter.WriteLineAsync(value);
            streamWriter.Close();
            await streamWriter.DisposeAsync();
        }
    }
}
