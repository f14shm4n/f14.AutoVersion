using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AutoVersion.Core
{
    public static class ArgumentList
    {
        private static List<Argument> _registred = new List<Argument>()
        {
            new BackupProjectJsonArgument(),
            new VersionTemplateArgument()
        };
        private static List<Argument> _currentList = new List<Argument>();

        public static void ParseArguments(string args)
        {
            ParseArguments(args.Split(' '));
        }

        public static void ParseArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string a = args[i];
                Argument argument = Registred.FirstOrDefault(x => x.Aliases.Any(xa => xa.Equals(a, StringComparison.OrdinalIgnoreCase)));
                if (argument != null)
                {
                    if (argument.HasValue)
                    {
                        i += 1;
                        argument.ParseValue(args[i]);
                    }
                    _currentList.Add(argument);
                }
            }
        }

        public static void DoActions()
        {
            _currentList.Sort();
            _currentList.ForEach(x => x.DoAction());
        }

        public static IEnumerable<Argument> Registred => _registred;
    }
}
