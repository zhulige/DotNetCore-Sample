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
        

        [DllImport("lib.dll", EntryPoint = "NewInterferDetector")]
        public static extern object NewInterferDetector();

        [DllImport("lib.dll", EntryPoint = "SetParams")]
        public static extern int SetParams();

        [DllImport("lib.dll", EntryPoint = "PushSample")]
        public static extern int PushSample(object obj,char rssi);

        [DllImport("lib.dll", EntryPoint = "GetInterfer")]
        public static extern double GetInterfer(object obj);

    }
}
