using System.Text.Json;

public class JsonSerializer<T> : ISerializer<T>
{
    private readonly JsonSerializerOptions _options;

    public JsonSerializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true 
        };
    }

    public string Serialize(T obj) => JsonSerializer.Serialize(obj, _options);

    public T Deserialize(string data) => JsonSerializer.Deserialize<T>(data, _options) 
        ?? throw new InvalidOperationException("Deserialization failed");
}