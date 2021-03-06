﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RepoDb.Extensions
{
    /// <summary>
    /// Contains the extension methods for string.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Joins an array string with a given separator.
        /// </summary>
        /// <param name="strings">The enumerable list of strings.</param>
        /// <param name="separator">The separator to be used.</param>
        /// <returns>A joined string from a given array of strings separated by the defined separator.</returns>
        public static string Join(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings);
        }

        /// <summary>
        /// Removes the database quotes from the string.
        /// </summary>
        /// <param name="value">The string value where the database quotes will be removed.</param>
        /// <returns></returns>
        public static string AsUnquoted(this string value)
        {
            var v = value?.IndexOf(".") >= 0 ? value.Split(".".ToCharArray()).Last() : value;
            return Regex.Replace(v, @"[\[\]']+", "");
        }

        /// <summary>
        /// Adds a quotes to the string.
        /// </summary>
        /// <param name="value">The string value where the database quotes will be added.</param>
        /// <param name="trim">The boolean value that indicates whether to trim the string first before unquoting.</param>
        /// <returns></returns>
        public static string AsQuoted(this string value, bool trim = false)
        {
            if (trim)
            {
                value = value.Trim();
            }
            if (!value.StartsWith("["))
            {
                value = string.Concat("[", value);
            }
            if (!value.EndsWith("]"))
            {
                value = string.Concat(value, "]");
            }
            return value;
        }

        /// <summary>
        /// Adds a quotes to the string as a database parameter.
        /// </summary>
        /// <param name="value">The string value where the database quotes will be added.</param>
        /// <param name="trim">The boolean value that indicates whether to trim the string first before unquoting.</param>
        /// <returns></returns>
        public static string AsQuotedParameter(this string value, bool trim = false)
        {
            if (trim)
            {
                value = value.Trim();
            }
            value = value.AsUnquoted().Replace(" ", "_");
            return value;
        }

        // AsEnumerable
        internal static IEnumerable<string> AsEnumerable(this string value)
        {
            yield return value;
        }

        // AsJoinQualifier
        internal static string AsJoinQualifier(this string value, string leftAlias, string rightAlias)
        {
            return string.Concat(leftAlias, ".", value.AsQuoted(), " = ", rightAlias, ".", value.AsQuoted());
        }

        // AsField
        internal static string AsField(this string value)
        {
            return value.AsQuoted();
        }

        // AsParameter
        internal static string AsParameter(this string value)
        {
            return string.Concat("@", value.AsUnquoted());
        }

        // AsAliasField
        internal static string AsAliasField(this string value, string alias)
        {
            return string.Concat(alias, ".", value.AsQuoted());
        }

        // AsParameterAsField
        internal static string AsParameterAsField(this string value)
        {
            return string.Concat(AsParameter(value), " AS ", AsField(value));
        }

        // AsFieldAndParameter
        internal static string AsFieldAndParameter(this string value)
        {
            return string.Concat(AsField(value), " = ", AsParameter(value));
        }

        // AsFieldAndAliasField
        internal static string AsFieldAndAliasField(this string value, string alias)
        {
            return string.Concat(AsField(value), " = ", alias, ".", AsField(value));
        }

        /* IEnumerable<string> */

        // AsFields
        internal static IEnumerable<string> AsFields(this IEnumerable<string> values)
        {
            return values?.Select(value => value.AsField());
        }

        // AsParameters
        internal static IEnumerable<string> AsParameters(this IEnumerable<string> values)
        {
            return values?.Select(value => value.AsParameter());
        }

        // AsAliasFields
        internal static IEnumerable<string> AsAliasFields(this IEnumerable<string> values, string alias)
        {
            return values?.Select(value => value.AsAliasField(alias));
        }

        // AsParametersAsFields
        internal static IEnumerable<string> AsParametersAsFields(this IEnumerable<string> values)
        {
            return values?.Select(value => value.AsParameterAsField());
        }

        // AsFieldsAndParameters
        internal static IEnumerable<string> AsFieldsAndParameters(this IEnumerable<string> values)
        {
            return values?.Select(value => value.AsFieldAndParameter());
        }

        // AsFieldsAndAliasFields
        internal static IEnumerable<string> AsFieldsAndAliasFields(this IEnumerable<string> values, string alias)
        {
            return values?.Select(value => value.AsFieldAndAliasField(alias));
        }
    }
}
