using System;
using System.Text.RegularExpressions;

namespace SecureByDesign
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A safe database name. </summary>
    ///-------------------------------------------------------------------------------------------------
    public class SafeDatabaseName
    {
        /// <summary>   A regex to validate a safe database name. </summary>
        private static readonly Regex SafeNameExpression = new Regex(@"^RT[a-z0-9]{1,62}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when the database source doesn't match the standard.
        ///                                         </exception>
        ///
        /// <param source="name"> The source. </param>
        ///-------------------------------------------------------------------------------------------------
        public SafeDatabaseName(string name)
        {
            this.Value = string.IsNullOrEmpty(name) ? null 
                : SafeNameExpression.Match(name).Success ? name 
                : throw new ArgumentException($"Invalid database source '{name}'");
        }

        #region properties

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the source. </summary>
        ///
        /// <source> The source. </source>
        ///-------------------------------------------------------------------------------------------------
        public string Value { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a source indicating whether this object has source. </summary>
        ///
        /// <source> True if this object has source, false if not. </source>
        ///-------------------------------------------------------------------------------------------------

        public bool HasValue => Value != null;
        #endregion

        #region operators

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Implicit cast that converts the given string to a SafeDatabaseName. </summary>
        ///
        /// <param source="source"> The source string. </param>
        ///
        /// <returns>   A SafeDatabaseName or an exception if the string isn't a valid database name. 
        ///             </returns>
        ///-------------------------------------------------------------------------------------------------

        public static implicit operator SafeDatabaseName(string source) => new SafeDatabaseName(source);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Implicit cast that converts the given SafeDatabaseName to a string. </summary>
        ///
        /// <param source="source">  The source SafeDatabaseName. </param>
        ///
        /// <returns>   The Value property of the SafeDatabaseName. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static implicit operator string(SafeDatabaseName source) => source.Value;
        #endregion
    }
}
