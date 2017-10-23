

namespace dBosque.Stub.WebApi.Configuration.Model
{
    /// <summary>
    /// A Regex stub
    /// </summary>
    public class RegexStub
    {
        /// <summary>
        /// The regex pattern to use
        /// The regular expression on which this stub reacts
        /// The .NET regular expression parser is used (https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)
        /// All named groups are extract and used as template restrictions (https://docs.microsoft.com/en-us/dotnet/standard/base-types/grouping-constructs-in-regular-expressions)
        /// </summary>
        public string Pattern
        {
            get;
            set;
        }

        /// <summary>
        /// A human readable name for the stub
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}