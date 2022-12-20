using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Files
{
    public class FileDeleteService
    {
        public static void DeleteFileIfExists(string route)
        {
            if (File.Exists(route))
            {
                File.Delete(route);
            }
        }
    }
}
