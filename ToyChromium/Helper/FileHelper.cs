using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyChromium.Helper
{
    class FileHelper
    {
        public List<string> GetFileNames(string path)
        {
            var filesPath = Directory.GetFiles(path);
            List<string> files = new List<string>();
            foreach (var file in filesPath)
                files.Add(Path.GetFileName(file));
            return files;
        }

        public string GetJsonFileName(string path)
        {
            return JsonConvert.SerializeObject(GetFileNames(path));
        }

        public void WriteTextFile(string filePath, string content)
        {
            //FileStream fs = File.OpenWrite(filePath);
            StreamWriter sw = new StreamWriter(filePath, false);
            sw.Write(content);
            sw.Close();
            //fs.Close();
        }
    }
}
