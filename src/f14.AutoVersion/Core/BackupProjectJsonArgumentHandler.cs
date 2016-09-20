using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AutoVersion.Core
{
    public class BackupProjectJsonArgumentHandler : ArgumentHandler
    {
        public BackupProjectJsonArgumentHandler()
        {
            Aliases = new string[] { "-backup", "-b" };
            Description = "Create backup file for project.json file with name project.json.bak.";
            Order = 1;
        }

        public override void DoAction()
        {
            File.Copy("project.json", "project.json.bak", true);
        }

        public override void ParseValue(string value)
        {
            
        }
    }
}
