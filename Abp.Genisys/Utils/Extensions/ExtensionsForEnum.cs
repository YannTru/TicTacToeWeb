using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Abp.Utils.Extensions
{
    static public class ExtensionsForEnum
    {
        public static string DisplayName(this Enum value)
        {
            //Using reflection to get the field info
            FieldInfo info = value.GetType().GetField(value.ToString());

            //Get the Description Attributes
            var attributes = (DisplayAttribute[])info.GetCustomAttributes(typeof(DisplayAttribute), false);

            //Only capture the description attribute if it is a concrete result (i.e. 1 entry)
            if (attributes.Length == 1)
            {
                return attributes[0].Name;
            }
            else //Use the value for display if not concrete result
            {
                return value.ToString();
            }
        }

        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>
            (this IEnumerable<TSource> source,
             Func<TSource, TSource, TResult> projection)
            {
                using (var iterator = source.GetEnumerator())
                {
                    if (!iterator.MoveNext())
                    {
                        yield break;
                    }
                    TSource previous = iterator.Current;
                    while (iterator.MoveNext())
                    {
                        yield return projection(previous, iterator.Current);
                        previous = iterator.Current;
                    }
                }
            }
    }


}