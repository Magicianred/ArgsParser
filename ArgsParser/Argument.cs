using System;

namespace ArgsParser
{
    public class Argument
    {

        #region Properties

        public string Name { get; set; }

        public Type Type { get; set; }

        public string Alias { get; set; }

        public string Help { get; set; }

        public bool HasValue { get; set; }

        public bool Optional { get; set; }

        public bool HasAlias => !string.IsNullOrEmpty(Alias);

        public bool HasHelp => !string.IsNullOrEmpty(Help);

        #endregion

        public Argument(string name, Type type, string alias, string help = null, bool hasValue = true, bool optional = false)
        {
            Name = name;
            Type = type;
            Alias = alias;
            Help = help;
            Optional = optional;
            HasValue = hasValue;
        }

    }
}
