using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgsParser
{
    public class ArgumentsParsed
    {
        
        #region Fields

        private readonly Dictionary<string, KeyValuePair<Argument, object>> _argumentsParsed;
        
        #endregion

        internal ArgumentsParsed()
        {
            _argumentsParsed = new Dictionary<string, KeyValuePair<Argument, object>>();
        }
        
        #region Internal Methods

        internal void AddParsedArgument(Argument argument, object value)
        {
            _argumentsParsed.Add(argument.Name, new KeyValuePair<Argument, object>(argument, value));
        }
        
        #endregion
        
        #region Public Methods

        public T GetArgumentValue<T>(string argumentName)
        {
            if (!_argumentsParsed.ContainsKey(argumentName))
                throw new ArgumentException($"The argument {argumentName} wasn't parsed.");

            return (T)_argumentsParsed[argumentName].Value;
        }

        public List<KeyValuePair<Argument, object>> GetAllParsedArguments() => _argumentsParsed.Values.ToList();

        public bool HasArgument(string argumentName) => _argumentsParsed.ContainsKey(argumentName);
        
        #endregion

    }
}