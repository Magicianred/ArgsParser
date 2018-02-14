using System;

namespace ArgsParser.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParser = new ArgumentsParser();
            argsParser.AddArgument(new Argument("-r", typeof(int), "--req", "A testing argument."));
            argsParser.AddArgument(new Argument("-o", typeof(string), "--opt", "A testing argument.", false, true));
            var argsParsed = argsParser.Parse(args);
            int t = argsParsed.GetArgumentValue<int>("-r");
            Console.WriteLine(t + 10);
            
            if (argsParsed.HasArgument("-o"))
                Console.WriteLine("-o is in!");
        }
    }
}
