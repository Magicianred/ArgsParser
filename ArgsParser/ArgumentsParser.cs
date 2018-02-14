using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void AddArgument(Argument argument)
        {
            if (_arguments.ContainsKey(argument.Name))
                throw new ArgumentException("The name of this argument is already added.");

            _arguments.Add(argument.Name, argument);
        }

        public ArgumentsParsed Parse(string[] args)
        {
            if (args.Contains("-h") || args.Contains("--help"))
            {
                PrintHelp();
                return null;
            }

            if (_arguments.Count == 0)
                return null;
            
            var argumentsParsed = new ArgumentsParsed();

            foreach (var arg in _arguments.Values)
            {
                int argIndex = Array.FindIndex(args, a => a == arg.Name || a == arg.Alias);
                
                // If the argument is required but wasn't found
                if (!arg.Optional && argIndex == -1)
                    throw new ArgumentException($"The argument '{arg}' is required.");

                if (!arg.HasValue)
                {
                    if (argIndex != -1)
                    {
                        // If the argument doesn't expect a value
                        argumentsParsed.AddParsedArgument(arg, null);
                    }

                    continue;
                }
                
                int valueIndex = argIndex + 1;
                    
                // If there is no value
                if (valueIndex == args.Length)
                    throw new ArgumentException($"The argument '{arg}' requires a value.");

                string strValue = args[valueIndex];
                object value = null;

                try
                {
                    value = ConvertStringToType(strValue, arg.Type);
                }
                catch
                {
                    throw new ArgumentException($"Invalid value of the argument '{arg}'.");
                }

                argumentsParsed.AddParsedArgument(arg, value);
            }

            return argumentsParsed;
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

            var sb = new StringBuilder();
            sb.AppendLine("Available arguments:");
            sb.AppendLine($"{FixedPadRight("\t-h, --help")}Shows the available arguments.");

            foreach (var arg in _arguments.Values)
            {
                var str = $"\t{arg.Name}";
                
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

        private static object ConvertStringToType(string str, Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            return converter.ConvertFromInvariantString(str);
        }

        private static string FixedPadRight(string str) => str.PadRight(20);

        #endregion

    }
}