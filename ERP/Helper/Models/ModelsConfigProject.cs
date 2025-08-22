using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

namespace ERP.Helper.Models
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public static class Serialize
    {
        public static string ToJson<T>(this ResponseExternalServiceModel<T> self) => JsonConvert.SerializeObject(self, ERP.Helper.Models.Converter.Settings);
        public static string ToJson(this SmtpSendRequestModel self) => JsonConvert.SerializeObject(self, ERP.Helper.Models.Converter.Settings);
    }
}
