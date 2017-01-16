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
        public static void Launch()
        {
            Process run = new Process();
            run.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            run.Start();
        }
    }
}
