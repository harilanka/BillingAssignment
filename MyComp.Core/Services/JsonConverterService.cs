using System;
using MyComp.Core.Interfaces;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyComp.Core.Services
{
    public class JsonConverterService : IJsonConverterService
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
        };

        public async Task<T> DeserializeAsync<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Input JSON cannot be null or empty");

            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var result = await JsonSerializer.DeserializeAsync<T>(jsonStream, _options);
            return result ?? throw new InvalidOperationException("Deserialization resulted in null");
        }

        public async Task<string> SerializeAsync<T>(T data)
        {
            using var memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memoryStream, data, _options);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream);
            return await reader.ReadToEndAsync();
        }
    }
}

