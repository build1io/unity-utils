#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;

namespace Build1.UnityUtils.Json
{
    public sealed class DictionaryNumericEnumKeysConverter : JsonConverter
    {
        public override bool CanRead  => false; // the default converter handles numeric keys fine
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return TryGetEnumType(objectType, out _);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException($"Reading isn't implemented by the {nameof(DictionaryNumericEnumKeysConverter)} converter."); // shouldn't be called since we set CanRead to false
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            if (value is not IDictionary dictionary || !TryGetEnumType(value.GetType(), out Type? enumType))
                throw new InvalidOperationException($"Can't parse value type '{value.GetType().FullName}' as a supported dictionary type."); // shouldn't be possible since we check in CanConvert
            
            var enumValueType = Enum.GetUnderlyingType(enumType);

            // serialize
            writer.WriteStartObject();
            foreach (DictionaryEntry pair in dictionary)
            {
                writer.WritePropertyName(Convert.ChangeType(pair.Key, enumValueType).ToString()!);
                serializer.Serialize(writer, pair.Value);
            }

            writer.WriteEndObject();
        }

        private bool TryGetEnumType(Type objectType, [NotNullWhen(true)] out Type? keyType)
        {
            if (!objectType.IsGenericType || objectType.IsValueType)
            {
                keyType = null;
                return false;
            }

            var genericType = objectType.GetGenericTypeDefinition();
            if (genericType != typeof(IDictionary<,>) && genericType != typeof(Dictionary<,>))
            {
                keyType = null;
                return false;
            }

            keyType = objectType.GetGenericArguments().First();
            if (!keyType.IsEnum)
                keyType = null;

            return keyType != null;
        }
    }
}