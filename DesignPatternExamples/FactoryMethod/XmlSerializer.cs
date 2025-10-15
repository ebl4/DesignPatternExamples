public class XmlSerializer<T> : ISerializer<T>
{
    public string Serialize(T obj)
    {
        // Implementação simplificada para exemplo
        return $"<root><data>{obj?.ToString()}</data></root>";
    }

    public T Deserialize(string data)
    {
        // Implementação simplificada
        return default(T)!;
    }
}