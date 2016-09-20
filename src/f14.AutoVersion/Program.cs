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
        private static ArgumentHandler[] _registeredHandlers =
        {
            new BackupProjectJsonArgumentHandler(),
            new VersionTemplateArgumentHandler()
        };
        private static List<ArgumentHandler> _handlersToExecution = new List<ArgumentHandler>();

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }

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
                ExecuteHandlers();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot execute some actions. See exception: " + ex.ToString());
            }
        }

        #region Public
        /// <summary>
        /// Parse arguments from string. Arguments must be separated by whitespace.
        /// </summary>
        /// <param name="args">Arguments string.</param>
        public static void ParseArguments(string args)
        {
            ParseArguments(args.Split(' '));
        }
        /// <summary>
        /// Parse arguments from array.
        /// </summary>
        /// <param name="args">Argument array.</param>
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
        /// <summary>
        /// Execute all argument handles which parsed from arguments array.
        /// </summary>
        public static void ExecuteHandlers()
        {
            if (_handlersToExecution.Count == 0)
            {
                PrintHelp();
                return;
            }

            _handlersToExecution.Sort();
            _handlersToExecution.ForEach(x => x.Execute());
        }

        #endregion

        #region Private

        private static void PrintHelp()
        {
            Console.WriteLine("How to use.");
            foreach (var a in _registeredHandlers)
            {
                Console.WriteLine($"Aliases: {string.Join(",", a.Aliases)} Description: {a.Description}");
            }
        }
        /// <summary>
        /// Get registered argument handler by command alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        private static ArgumentHandler GetArgumentHandler(string alias)
        {
            return _registeredHandlers.FirstOrDefault(x => x.Aliases.Any(xa => xa.Equals(alias, StringComparison.OrdinalIgnoreCase)));
        }

        #endregion
    }
}
