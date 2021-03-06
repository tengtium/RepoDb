﻿using System.Collections.Generic;

namespace RepoDb.Extensions
{
    /// <summary>
    /// Contains the extension methods for <see cref="QueryGroup"/> object.
    /// </summary>
    public static class QueryGroupExtension
    {
        /// <summary>
        /// Convert an instance of query group into an enumerable list of query groups.
        /// </summary>
        /// <param name="queryGroup">The <see cref="QueryGroup"/> object to be converted.</param>
        /// <returns>An enumerable list of query groups.</returns>
        public static IEnumerable<QueryGroup> AsEnumerable(this QueryGroup queryGroup)
        {
            yield return queryGroup;
        }

        /// <summary>
        /// Maps the current <see cref="QueryGroup"/> object to a type.
        /// </summary>
        /// <param name="queryGroup">The <see cref="QueryGroup"/> object to be mapped.</param>
        /// <typeparam name="T">The target type where the current <see cref="QueryGroup"/> is to be mapped.</typeparam>
        /// <returns>An instance of <see cref="QueryGroupTypeMap"/> object that holds the mapping.</returns>
        internal static QueryGroupTypeMap MapTo<T>(this QueryGroup queryGroup) where T : class
        {
            return new QueryGroupTypeMap(queryGroup, typeof(T));
        }
    }
}
