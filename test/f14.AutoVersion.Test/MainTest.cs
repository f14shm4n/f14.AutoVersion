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
    public class MainTest
    {
        private Exception SimVersionTemplateArgument(string template)
        {
            Exception e = null;
            try
            {
                string[] args = { "-t", template };
                for (int i = 0; i < args.Length; i++)
                {
                    string a = args[i];
                    VersionTemplateArgument vta = new VersionTemplateArgument();
                    if (vta.HasValue)
                    {
                        i += 1;
                        vta.ParseValue(args[i]);
                        vta.DoAction();
                    }
                }
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
                BackupProjectJsonArgument bck = new BackupProjectJsonArgument();
                bck.DoAction();
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
            //ArgumentList("-b -t 1.0.0-b{0}");
            foreach(var a in ArgumentList.Registred)
            {
                Debug.WriteLine("Type: " + a);
            }            
            Assert.IsTrue(true);
        }
    }
}
