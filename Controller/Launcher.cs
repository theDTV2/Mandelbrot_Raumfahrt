using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class Launcher
    {
        public static Process StartServerByOS()
        {
            return Process.Start(ProcessVariables.GetServerVariable());
        }

        public static Process StartClientByOS()
        {
            return Process.Start(ProcessVariables.GetClientVariable());
        }

    }
}
