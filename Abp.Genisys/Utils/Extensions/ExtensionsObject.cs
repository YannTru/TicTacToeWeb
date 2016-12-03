using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


    public static class ObjectExtensions
    {
        public static string ToJson(this object obj, bool formatted = false)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        Formatting = (formatted? Formatting.Indented : Formatting.None)
                    });
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return "errorToJson = " + ex.Message;
            }
        }

        public static bool ToBool(this object obj)
        {
            try
            {
                if (obj == System.DBNull.Value ||
                    obj == null ||
                    (obj is string && string.IsNullOrEmpty(obj as string)) ||
                    obj.GetType() == typeof(Object))
                    return false;
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ToDate(this object obj)
        {
            if (obj == System.DBNull.Value)
                return DateTime.Now;

            DateTime MyDate;
            try
            {
                MyDate = Convert.ToDateTime(obj);
            }
            catch
            {
                MyDate = DateTime.Now;
            }
            return MyDate;
        }

        public static DateTime? ToNullableDate(this object obj)
        {
            if (obj == System.DBNull.Value)
                return null;

            DateTime? MyDate;
            try
            {
                MyDate = Convert.ToDateTime(obj);
            }
            catch
            {
                MyDate = null;
            }
            return MyDate;
        }

        public static decimal ToDecimal(this object obj)
        {
            if (obj == System.DBNull.Value ||
                obj == null ||
                (obj is string && string.IsNullOrEmpty(obj as string)) ||
                obj.GetType() == typeof(Object))
                return 0;

            decimal myDecimal;

            try
            {
                if (obj is string && (obj.ToString().Contains(",") || obj.ToString().Contains(".")))
                {
                    myDecimal = ((float)0.1).ToString().Contains(",") ? Convert.ToDecimal(((String)obj).Replace(".", ",")) : Convert.ToDecimal(((String)obj).Replace(",", "."));
                }
                else
                {
                    myDecimal = Convert.ToDecimal(obj);
                }
            }
            catch
            {
                myDecimal = 0;
            }
            return myDecimal;
        }

        public static int ToInt(this object obj)
        {
            int MyInt;

            try
            {
                if (obj == System.DBNull.Value ||
                    obj == null ||
                    (obj is string && string.IsNullOrEmpty(obj as string)) ||
                    obj.GetType() == typeof(Object))
                    return 0;

                if (obj is string)
                {
                    if (obj.ToString().ToLower() == "false")
                        return 0;
                    else if (obj.ToString().ToLower() == "true")
                        return 1;
                }



                MyInt = Convert.ToInt32(obj);
            }
            catch
            {
                MyInt = 0;
            }
            return MyInt;
        }

        /// <summary>
        /// Set all DateTime and DateTime? properties on th eobject to DateTimeKind.UTC.
        /// ***Note: it does not goes through the whole object graph. Only 'first level' properties are assigned.
        /// ***Note: The problem this try to fix is that EntityFramework set DateTime properties with a DateTimeKind.Unspecified.
        ///         This cause the JSON serialization to not be UTC (missing 'Z').
        ///         So at first I was thinking of of calling this method on each Dto returned to client, but finally I changed the 
        ///         SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc in PMPWebApiModule.Initialize().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void SetDateTimesAsUtc<T>(this T obj) where T : class
        {
            Type t = obj.GetType();

            // Loop through the properties.
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props)
            {
                // If a property is DateTime or DateTime?, set DateTimeKind to DateTimeKind.Utc.
                if (p.PropertyType == typeof(DateTime))
                {
                    DateTime date = (DateTime)p.GetValue(obj, null);
                    date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                    p.SetValue(obj, date, null);
                }
                // Same check for nullable DateTime.
                else if (p.PropertyType == typeof(Nullable<DateTime>))
                {
                    DateTime? date = (DateTime?)p.GetValue(obj, null);
                    if (date.HasValue)
                    {
                        DateTime? newDate = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
                        p.SetValue(obj, newDate, null);
                    }
                }
            }
        }
    }
