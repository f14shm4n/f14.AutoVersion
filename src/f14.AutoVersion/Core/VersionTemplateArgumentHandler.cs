using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace f14.AutoVersion.Core
{
    public class VersionTemplateArgumentHandler : ArgumentHandler
    {
        public VersionTemplateArgumentHandler()
        {
            Aliases = new string[] { "-template", "-t" };
            Description = "Set the application version template. Sample: 1.0.0-beta-{000000}.";
            HasValue = true;
            Order = 9999;
        }                      

        public override void ParseValue(string value)
        {
            Value = value;
        }

        public override void DoAction()
        {
            JObject projectJson = ReadProjectJson();
            ChangeVersion(projectJson);
            WriteProjectJson(projectJson);
        }

        private JObject ReadProjectJson()
        {
            using (var stream = new FileStream("project.json", FileMode.Open, FileAccess.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    return JObject.Parse(reader.ReadToEnd());
                }
            }
        }

        private void WriteProjectJson(JObject projectJson)
        {
            using (var stream = new FileStream("project.json", FileMode.Create, FileAccess.ReadWrite))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(projectJson.ToString());
                }
            }
        }

        private void ChangeVersion(JObject projectJson)
        {
            string template = Convert.ToString(Value);
            int start = template.IndexOf('{') + 1;
            int length = template.IndexOf('}') - start;
            string tmp = template.Substring(start, length);
            string pattern = template.Replace("{" + tmp + "}", @"(?<ver>\d+)");
            Regex rgx = new Regex(pattern);

            string s_version = projectJson.Value<string>("version");

            var m = rgx.Match(s_version);
            if (m.Success)
            {
                int v = Convert.ToInt32(m.Groups["ver"].Value) + 1;
                string new_version = template.Replace("{" + tmp + "}", v.ToString(tmp.Aggregate("", (t, n) => t += "0")));
                projectJson["version"] = new_version;
            }
            else
            {
                string new_version = template.Replace("{" + tmp + "}", 1.ToString(tmp.Aggregate("", (t, n) => t += "0")));
                projectJson["version"] = new_version;
            }
        }
    }
}
