using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class WeatherForecastDTOInputSerializer
        : IInputSerializer
    {
        private bool _needsInitialization = true;
        private IValueSerializer _dateTimeSerializer;
        private IValueSerializer _stringSerializer;
        private IValueSerializer _intSerializer;

        public string Name { get; } = "WeatherForecastDTOInput";

        public ValueKind Kind { get; } = ValueKind.InputObject;

        public Type ClrType => typeof(WeatherForecastDTOInput);

        public Type SerializationType => typeof(IReadOnlyDictionary<string, object>);

        public void Initialize(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _dateTimeSerializer = serializerResolver.Get("DateTime");
            _stringSerializer = serializerResolver.Get("String");
            _intSerializer = serializerResolver.Get("Int");
            _needsInitialization = false;
        }

        public object Serialize(object value)
        {
            if (_needsInitialization)
            {
                throw new InvalidOperationException(
                    $"The serializer for type `{Name}` has not been initialized.");
            }

            if (value is null)
            {
                return null;
            }

            var input = (WeatherForecastDTOInput)value;
            var map = new Dictionary<string, object>();

            if (input.Date.HasValue)
            {
                map.Add("date", SerializeNullableDateTime(input.Date.Value));
            }

            if (input.Summary.HasValue)
            {
                map.Add("summary", SerializeNullableString(input.Summary.Value));
            }

            if (input.TemperatureC.HasValue)
            {
                map.Add("temperatureC", SerializeNullableInt(input.TemperatureC.Value));
            }

            if (input.TemperatureF.HasValue)
            {
                map.Add("temperatureF", SerializeNullableInt(input.TemperatureF.Value));
            }

            return map;
        }

        private object SerializeNullableDateTime(object value)
        {
            return _dateTimeSerializer.Serialize(value);
        }
        private object SerializeNullableString(object value)
        {
            return _stringSerializer.Serialize(value);
        }
        private object SerializeNullableInt(object value)
        {
            return _intSerializer.Serialize(value);
        }

        public object Deserialize(object value)
        {
            throw new NotSupportedException(
                "Deserializing input values is not supported.");
        }
    }
}
