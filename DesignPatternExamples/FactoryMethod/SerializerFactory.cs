// Factory
public static class SerializerFactory
{
    public static ISerializer<T> CreateSerializer<T>(SerializerType type)
    {
        return type switch
        {
            SerializerType.Json => new JsonSerializer<T>(),
            SerializerType.Xml => new XmlSerializer<T>(),
            _ => throw new ArgumentException($"Unsupported serializer type: {type}")
        };
    }
}