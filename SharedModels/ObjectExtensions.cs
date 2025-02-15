using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType?
                         .GetProperty(item.Key)?
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object?> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

        public static Dictionary<string, object> AsDictionary(this IEnumerable<Claim> claims)
        {
            var dic = new Dictionary<string, object>();
            foreach (Claim claim in claims)
            {
                dic.Add(claim.Type, claim.Value.ToString());
            }

            return dic;
        }

        public static string? GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();

                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }

        public static string ToBase64(this MemoryStream stream)
        {
            stream.Position = 0;
            var fileBytes = stream.ToArray();
            string base64 = fileBytes.ToBase64();

            return base64;
        }

        public static string ToBase64(this Byte[] bytes)
        {
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static int ToInt(this Enum enumValue)
        {
            return (int)((object)enumValue);
        }

        public static string AsString(this Enum enumValue)
        {
            return (string)((object)enumValue);
        }

        public static T ToNumeric<T>(this Enum enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (T)Convert.ChangeType(enumValue, typeof(T));
        }

        public static byte[]? SerializeObjToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }


            if (obj.GetType() == typeof(JObject))
            {

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    using (var writer = new StreamWriter(ms))
                    {
                        using (var jsonWriter = new JsonTextWriter(writer))
                        {
                            JObject.FromObject(obj).WriteTo(jsonWriter);
                        }
                    }
                    bytes = ms.ToArray();
                    return bytes;
                }
            }

            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj);


            //var bf = new BinaryFormatter();
            //using (var ms = new MemoryStream())
            //{
            //    bf.Serialize(ms, obj);
            //    return ms.ToArray();
            //}
        }

        public static T DeserializeToObj<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return null;
            }

            if (typeof(T) == typeof(JObject))
            {

                dynamic obj2;
                using (var ms = new MemoryStream(byteArray))
                {
                    using (var reader = new StreamReader(ms))
                    {
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            obj2 = JObject.Load(jsonReader);
                            return (T)obj2;
                        }
                    }
                }
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(byteArray);

            //using (var memStream = new MemoryStream())
            //{
            //    var binForm = new BinaryFormatter();
            //    memStream.Write(byteArray, 0, byteArray.Length);
            //    memStream.Seek(0, SeekOrigin.Begin);
            //    var obj = (T)binForm.Deserialize(memStream);
            //    return obj;
            //}
        }

        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }

        public static List<PropertyInfo> GetPropertiesWithAttr<T>(this object OBJ) where T : class
        {
            var props = OBJ.GetType().GetProperties()
              .Where(
              prop => Attribute.IsDefined(prop, typeof(T))).ToList();

            return props;
        }

        private static void AddToDictionary(IDictionary<string, object> dictionary, string[] keyParts, string value)
        {
            if (keyParts.Length == 1)
            {
                dictionary[keyParts[0]] = value;
                return;
            }

            var currentKeyPart = keyParts[0];
            var currentDictionary = dictionary.ContainsKey(currentKeyPart)
                ? (IDictionary<string, object>)dictionary[currentKeyPart]
                : new Dictionary<string, object>();

            AddToDictionary(currentDictionary, keyParts.Skip(1).ToArray(), value);

            dictionary[currentKeyPart] = currentDictionary;
        }


        public static DataTable ToDataTable<T>(this IEnumerable<T> objects) where T : class
        {
            DataTable dataTable = new DataTable();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create DataTable columns
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                dataTable.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            // Add rows to DataTable
            foreach (T obj in objects)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(obj, null) ?? DBNull.Value;
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public static DataTable ToDataTable(this IEnumerable<object> objects)
        {
            DataTable dataTable = new DataTable();

            if (objects == null || !objects.Any())
                return dataTable;

            var properties = GetPropertiesFromObject(objects.FirstOrDefault());
            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            foreach (object obj in objects)
            {
                var values = GetPropertyValues(obj);
                var row = dataTable.NewRow();

                foreach (var property in properties)
                {
                    row[property.Name] = values.TryGetValue(property.Name, out object value) ? value ?? DBNull.Value : DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesFromObject(object obj)
        {
            return obj?.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? Enumerable.Empty<PropertyInfo>();
        }

        private static Dictionary<string, object?> GetPropertyValues(object obj)
        {
            var properties = GetPropertiesFromObject(obj);

            return properties.ToDictionary(
                property => property.Name,
                property => property.GetValue(obj, null));
        }

        public static T? ConvertTo<T>(this string strValue)
        {
            /// Use TypeConverter to perform dynamic casting.
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.CanConvertFrom(typeof(string)))
            {
                return (T?)converter.ConvertFromString(strValue);
            }

            // If conversion is not supported, return default value for T.
            return default;
        }

        public static bool HasValue(this string? value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static bool HasValue<T>(this T? value, bool zeroIsNotValue = false) where T : struct
        {
            if (zeroIsNotValue)
            {
                return value.HasValue && !value.Equals(0);
            }

            return value.HasValue;
        }

        public static T? IfTrue<T>(this bool value, Func<T> action) => value ? action() : default;

        public static T? IfTrue<T>(this bool value, T trueValue) => value ? trueValue : default;

        public static void IfTrue(this bool value, Action action) { if (value) action(); }
        public static async Task<T?> IfTrueAsync<T>(this bool value, Func<Task<T>> trueAction) => value ? await trueAction() : default;

        public static async Task<T?> IfTrueAsync<T>(this bool value, T trueValue) => value ? await Task.FromResult(trueValue) : default;

        public static async Task IfTrueAsync(this bool value, Func<Task> trueAction) { if (value) await trueAction(); }

        public static Dictionary<TKey, TValue> DeepCopy<TKey, TValue>(this Dictionary<TKey, TValue> source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            var copy = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(serialized);
            return copy;
        }

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> source, Action<TKey, TValue> action)
        {
            foreach (var kvp in source)
            {
                action(kvp.Key, kvp.Value);
            }
        }

        public static async Task ForEachAsync<TKey, TValue>(this Dictionary<TKey, TValue> source, Func<TKey, TValue, Task> asyncAction)
        {
            foreach (var kvp in source)
            {
                await asyncAction(kvp.Key, kvp.Value);
            }
        }

        public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T>? source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static void TryFunc(Action action, Exception? defaultException = null)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as per your requirements
                Console.WriteLine($"Exception occurred: {ex.Message}");

                // Throw the default exception if provided
                if (defaultException != null)
                {
                    throw defaultException;
                }
            }
        }

        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
    }
}
