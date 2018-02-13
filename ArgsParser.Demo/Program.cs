using System;

namespace ArgsParser.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParser = new ArgumentsParser();
            argsParser.AddArgument(new Argument("-t", typeof(int), "--test", "A testing argument."));
            argsParser.Parse(args);
        }
    }
}
