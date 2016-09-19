using f14.AutoVersion.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AutoVersion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ArgumentList.ParseArguments(args);
            ArgumentList.DoActions();
        }
    }
}
