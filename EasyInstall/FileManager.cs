using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyInstall
{
    class FileManager
    {
        public static void Launch(string path)
        {
            Process run = new Process();
            run.StartInfo.FileName = path;
            run.Start();
        }
    }
}
