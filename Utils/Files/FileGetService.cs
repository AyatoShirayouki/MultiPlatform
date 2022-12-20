using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Files
{
    public class FileGetService
    {
        public static byte[] GetFile(string path)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return bytes;
        }
    }
}
