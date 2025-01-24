using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BuddysKitchen.Core
{
    public static class ObjectExtensions
    {
        public static TResult Copy<TSource, TResult>(this TSource input) where TResult : new()
        {
            return input.Copy(new TResult());
        }

        public static TResult Copy<TSource, TResult>(this TSource input, TResult output, params string[] skipFields)
            where TResult : new()
        {
            return input.Copy(output, true, skipFields);
        }

        public static TResult Copy<TSource, TResult>(this TSource input, TResult output, bool skipKeys = true,
            params string[] skipFields) where TResult : new()
        {
            if (input == null)
            {
                return default;
            }

            if (output == null)
            {
                output = new TResult();
            }

            var sourceProperties = input.GetType().GetProperties().Where(p => p.CanRead);
            var destinationProperties = output.GetType().GetProperties()
                .Where(p => sourceProperties.Select(s => s.Name).Contains(p.Name) && p.CanWrite);

            foreach (var dest in destinationProperties)
            {
                var source =
                    sourceProperties.FirstOrDefault(p => p.Name == dest.Name && p.PropertyType == dest.PropertyType);
                if (source != null && !skipFields.Select(f => f.ToLower()).Contains(source.Name.ToLower()))
                {
                    if (skipKeys && IsKey(dest))
                    {
                        continue;
                    }

                    var sourceValue = source.GetValue(input);
                    var destinationValue = dest.GetValue(output);
                    if (sourceValue != destinationValue)
                    {
                        dest.SetValue(output, sourceValue);
                    }
                }
            }

            return output;
        }

        private static bool IsKey(PropertyInfo dest)
        {
            var attribute = Attribute.GetCustomAttribute(dest, typeof(KeyAttribute)) as KeyAttribute;
            return attribute != null;
        }

        public static long GetKey(this object input)
        {
            var propertyInfo = input.GetType().GetProperties().FirstOrDefault(IsKey);

            if (propertyInfo != null)
            {
                return (long)propertyInfo.GetValue(input);
            }

            return 0;
        }
    }
}
