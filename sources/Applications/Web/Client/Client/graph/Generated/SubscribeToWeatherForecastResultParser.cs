using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Transport;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SubscribeToWeatherForecastResultParser
        : JsonResultParserBase<ISubscribeToWeatherForecast>
    {
        private readonly IValueSerializer _dateTimeSerializer;
        private readonly IValueSerializer _intSerializer;
        private readonly IValueSerializer _stringSerializer;

        public SubscribeToWeatherForecastResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _dateTimeSerializer = serializerResolver.Get("DateTime");
            _intSerializer = serializerResolver.Get("Int");
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override ISubscribeToWeatherForecast ParserData(JsonElement data)
        {
            return new SubscribeToWeatherForecast
            (
                ParseSubscribeToWeatherForecastOnGetForecasts(data, "onGetForecasts")
            );

        }

        private global::LabTest2.Apps.Web.Client.GraphClient.IWeatherForecast ParseSubscribeToWeatherForecastOnGetForecasts(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new WeatherForecast
            (
                DeserializeDateTime(obj, "date"),
                DeserializeInt(obj, "temperatureC"),
                DeserializeInt(obj, "temperatureF"),
                DeserializeString(obj, "summary")
            );
        }

        private System.DateTimeOffset DeserializeDateTime(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (System.DateTimeOffset)_dateTimeSerializer.Deserialize(value.GetString());
        }

        private int DeserializeInt(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (int)_intSerializer.Deserialize(value.GetInt32());
        }

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString());
        }
    }
}
