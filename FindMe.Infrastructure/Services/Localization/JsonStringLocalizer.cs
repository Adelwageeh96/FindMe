using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace FindMe.Infrastructure.Services.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly JsonSerializer? _serializer = new();
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;

        public JsonStringLocalizer(
            IConfiguration configuration, IWebHostEnvironment host)
        {
            _configuration = configuration;
            _host = host;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {

            get
            {
                var actualvalue = this[name];
                return !actualvalue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualvalue.Value, arguments)
                    ) : actualvalue;
            }

        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);

            using JsonTextReader reader = new(streamReader);
            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName) continue;

                var key = reader.Value as string ?? "";

                reader.Read();

                var value = _serializer?.Deserialize<string>(reader);
                yield return new LocalizedString(key, value!);

            }

        }
        private string GetString(string key)
        {
            var filePath = $"{_host.WebRootPath}/Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath);

            if (File.Exists(fullFilePath))
            {
                var result = GetValueFromJson(key, fullFilePath);
                return result;
            }

            return string.Empty;
        }

        private string GetValueFromJson(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) ||
             string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);

            using JsonTextReader reader = new(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propertyName)
                {
                    reader.Read();

                    return _serializer?.Deserialize<string>(reader) ?? "";

                }

            }
            return string.Empty;
        }



    }
}
