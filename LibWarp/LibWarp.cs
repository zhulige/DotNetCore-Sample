using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LibWarp
{
    public class LibWarp
    {
        [DllImport("lib.dll", EntryPoint = "GSuccess")]
        public static extern int GSuccess();

        public int GetSuccess()
        {
            return GSuccess();
            
        }
    }
}
