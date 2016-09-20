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
        private static ArgumentHandler[] _registredHandlers =
        {
            new BackupProjectJsonArgumentHandler(),
            new VersionTemplateArgumentHandler()
        };
        private static List<ArgumentHandler> _handlersToExecution = new List<ArgumentHandler>();

        public static void Main(string[] args)
        {
            try
            {
                ParseArguments(args);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot parse arguments. See exception: " + ex.ToString());
                return;
            }

            try
            {
                DoActions();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot execute some actions. See exception: " + ex.ToString());
            }
        }

        #region Public

        public static void ParseArguments(string args)
        {
            ParseArguments(args.Split(' '));
        }

        public static void ParseArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string alias = args[i];
                ArgumentHandler argument = GetArgumentHandler(alias);
                if (argument != null)
                {
                    if (argument.HasValue)
                    {
                        i += 1;
                        argument.ParseValue(args[i]);
                    }
                    _handlersToExecution.Add(argument);
                }
            }
        }

        public static void DoActions()
        {
            if (_handlersToExecution.Count == 0)
            {
                Console.WriteLine("How to use.");
                foreach (var a in _registredHandlers)
                    Console.WriteLine($"Aliases: ${string.Join(",", a.Aliases)} Description: ${a.Description}");
                return;
            }

            _handlersToExecution.Sort();
            _handlersToExecution.ForEach(x => x.DoAction());
        }

        #endregion

        #region Private

        private static ArgumentHandler GetArgumentHandler(string alias)
        {
            return _registredHandlers.FirstOrDefault(x => x.Aliases.Any(xa => xa.Equals(alias, StringComparison.OrdinalIgnoreCase)));
        }

        #endregion
    }
}
