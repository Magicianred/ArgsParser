# ArgsParser

A command line arguments parser in .NET Core.

## How to use

### Register the arguments you want

    var argsParser = new ArgumentsParser();
    argsParser.AddArgument(new Argument("-r", typeof(int), "--req", "A required argument."));
    argsParser.AddArgument(new Argument("-o", typeof(string), "--opt", "An optional argument that doesn't have a value.", false, true));

This will tell the ArgumentsParser that `-r, --req` is a required argument that has a value of type int (e.g. `-r 3`) and that `-o, --opt` is an optional argument that doesn't have a value (e.g. `-o`).

### Parse the arguments

All you have to do is call the `Parse` method with the `string[] args` given to you on the main method.

    try
    {
	    var argsParsed = argsParser.Parse(args);
	}
	catch (Exception ex)
	{
		// You'll probably want to exit if the parsing fails
		Console.WriteLine(ex.Message);
        Environment.Exit(1);
	}

The parsing will fail if:

 - A required argument is missing.
 - An argument with `HasValue = true` but no value was provided.
 - An error occured when trying to convert the value to the specified type.

### Using the arguments

To use the arguments you have 2 main methods:

    // GetArgumentValue needs the argument's name
    int t = argsParsed.GetArgumentValue<int>("-t");
    
    // If you have an argument with HasValue = false
    if (argsParsed.HasArgument("-o"))
	    Console.WriteLine("-o, --o was specified!");

### Help

ArgsParser automatically provides a help argument:

    dotnet run -- yourapp -h
    dotnet run -- yourapp --help
    
    Available arguments:
		-h, --help               Shows the available arguments.
		-r, --req                A required argument.
		-o, --opt (optional)     An optional argument that doesn't have a value.

**This will print a nicely formated message containing the available arguments and will not parse anything else and return `null`.**
