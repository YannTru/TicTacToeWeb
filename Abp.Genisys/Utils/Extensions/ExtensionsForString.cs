using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Abp.Utils.Extensions
{
    public static class ExtensionsForString
    {
        public static string DefaultIfNull(this string str, string defaultValue = "")
        {
            return str ?? defaultValue;
        }

        public static object Deserialize(this string obj, object errorObj =null)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(obj,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            }
            catch (Exception)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return errorObj;
            }
        }

        public static string SpaceBeforeUpperCase(this string str)
        {
            var output = new StringBuilder();

            foreach (var letter in str)
            {
                if (char.IsUpper(letter) && output.Length > 0)
                    output.Append(" " + letter);
                else
                    output.Append(letter);
            }

            return output.ToString();
        }

        /// <summary>
        /// Compare 2 strings ignoring case, white spaces, line breaks
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool SmartCompare(this string s1, string s2)
        {
            if (s1 == null || s2 == null) return false;

            if (s1.ToLower() == s2.ToLower()) return true;

            // Remove white spaces, line breaks
            var s1Clean = Regex.Replace(s1.Trim().ToLower(), @"\r\n?|\n|\s", "");
            var s2Clean = Regex.Replace(s2.Trim().ToLower(), @"\r\n?|\n|\s", "");

            return s1Clean == s2Clean;
        }

        public static List<int> ToIntList(this string stringList, char separator = ',')
        {
            if (string.IsNullOrWhiteSpace(stringList)) return new List<int>();
            var list = stringList.Split(separator).Select(x => x.ToInt()).ToList();
            return list;
        }
    }
}