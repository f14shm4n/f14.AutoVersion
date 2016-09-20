using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using f14.AutoVersion.Core;
using System.Diagnostics;

namespace f14.AutoVersion.Test
{
    [TestClass]
    public class Main
    {
        private Exception SimVersionTemplateArgument(string template)
        {
            Exception e = null;
            try
            {
                VersionTemplateArgumentHandler vta = new VersionTemplateArgumentHandler();
                vta.ParseValue(template);
                vta.Execute();
            }
            catch (Exception ex)
            {
                e = ex;
            }
            if (e != null)
                Debug.WriteLine(e.ToString());
            return e;
        }

        [TestMethod]
        public void ChangeVersion_1()
        {
            Assert.IsNull(SimVersionTemplateArgument("1.0.0-beta-{000000}"));
        }

        [TestMethod]
        public void ChangeVersion_2()
        {
            Assert.IsNull(SimVersionTemplateArgument("1.0.0-b{0}"));
        }

        [TestMethod]
        public void TestBackupProjectJson()
        {
            Exception e = null;
            try
            {
                BackupProjectJsonArgumentHandler bck = new BackupProjectJsonArgumentHandler();
                bck.Execute();
            }
            catch (Exception ex)
            {
                e = ex;
            }
            if (e != null)
                Debug.WriteLine(e.ToString());
            Assert.IsNull(e);
        }

        [TestMethod]
        public void TestManyArguments()
        {            
            Exception e = null;
            try
            {
                Program.ParseArguments("-b -t 1.0.0-b{0}");
                Program.ExecuteHandlers();
            }
            catch (Exception ex)
            {
                e = ex;
            }
            if (e != null)
                Debug.WriteLine(e.ToString());
            Assert.IsNull(e);
        }
    }
}
