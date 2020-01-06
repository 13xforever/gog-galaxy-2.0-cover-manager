using System;
using System.Text;

namespace GogGalaxy20MetaManager
{
    public static class NamingStyles
    {
        public static string CamelCase(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length > 0)
            {
                if (char.IsUpper(value[0]))
                    value = char.ToLower(value[0]) + value.Substring(1);
            }
            return value;
        }

        public static string Dashed(string value)
        {
            return Delimited(value, '-');
        }

        public static string Underscore(string value)
        {
            return Delimited(value, '_');
        }

        private static string Delimited(string value, char separator)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length == 0)
                return value;

            var hasPrefix = true;
            var builder = new StringBuilder(value.Length + 3);
            foreach (var c in value)
            {
                var ch = c;
                if (char.IsUpper(ch))
                {
                    ch = char.ToLower(ch);
                    if (!hasPrefix)
                        builder.Append(separator);
                    hasPrefix = true;
                }
                else
                    hasPrefix = false;
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}