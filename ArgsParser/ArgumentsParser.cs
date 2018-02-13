using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgsParser
{
    public class ArgumentsParser
    {

        #region Fields

        private readonly Dictionary<string, Argument> _arguments;

        #endregion

        public ArgumentsParser()
        {
            _arguments = new Dictionary<string, Argument>();
        }

        #region Public Methods

        public Argument AddArgument(Argument argument)
        {
            if (_arguments.ContainsKey(argument.Name))
                throw new ArgumentException("The name of this argument is already added.");

            _arguments.Add(argument.Name, argument);
            return argument;
        }

        public void Parse(string[] args)
        {
            if (args.Contains("-h") || args.Contains("--help"))
            {
                PrintHelp();
                return;
            }
        }

        #endregion

        #region Private Methods

        private void PrintHelp()
        {
            if (_arguments.Count == 0)
            {
                Console.WriteLine("No arguments available.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Available arguments:");
            sb.AppendLine($"{FixedPadRight("\t-h, --help")}Shows the available arguments.");

            foreach (Argument arg in _arguments.Values)
            {
                string str = $"\t{arg.Name}";
                
                if (arg.HasAlias)
                {
                    str += $", {arg.Alias}";
                }

                sb.Append(FixedPadRight(str));

                if (arg.Optional)
                {
                    sb.Append(" (optional)");
                }

                if (arg.HasHelp)
                {
                    sb.Append($"{arg.Help}");
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        private static string FixedPadRight(string str) => str.PadRight(20);

        #endregion

    }
}