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
    public partial class GetWeatherForecastsResultParser
        : JsonResultParserBase<IGetWeatherForecasts>
    {
        private readonly IValueSerializer _stringSerializer;

        public GetWeatherForecastsResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override IGetWeatherForecasts ParserData(JsonElement data)
        {
            return new GetWeatherForecasts
            (
                ParseGetWeatherForecastsForecasts(data, "forecasts")
            );

        }

        private global::LabTest2.Apps.Web.Client.GraphClient.IGetForecastResponse ParseGetWeatherForecastsForecasts(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new GetForecastResponse
            (
                DeserializeString(obj, "response"),
                ParseGetWeatherForecastsForecastsErrors(obj, "errors")
            );
        }

        private global::System.Collections.Generic.IReadOnlyList<global::LabTest2.Apps.Web.Client.GraphClient.IUserError> ParseGetWeatherForecastsForecastsErrors(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new global::LabTest2.Apps.Web.Client.GraphClient.IUserError[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new UserError
                (
                    DeserializeString(element, "message")
                );

            }

            return list;
        }

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString());
        }
    }
}
