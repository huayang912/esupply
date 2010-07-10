/*
 * Created By Vincent On 2006-09-04
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Utility
{
    /// <summary>
    /// Sql Parameter Helper
    /// </summary>
    public sealed class DataParameterHelper
    {
        public const string PARAMETER_PREFIX = "<$";
        public const string PARAMETER_POSTFIX = "$>";

        /// <summary>
        /// Get Paramter Placeholder with the prefix and postfix
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetParameterPlaceholder(string param)
        {
            string result = string.Empty;
            if (param.Trim() != string.Empty)
            {
                result = PARAMETER_PREFIX + param.Trim() + PARAMETER_POSTFIX;
            }
            return result;
        }
    }
}
