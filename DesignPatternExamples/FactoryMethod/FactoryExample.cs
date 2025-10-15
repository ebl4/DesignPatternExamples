// Uso da Factory
public class FactoryExample
{
    public static void Execute()
    {
        var jsonSerializer = SerializerFactory.CreateSerializer<Person>(SerializerType.Json);
        var person = new Person("John", 30);

        var json = jsonSerializer.Serialize(person);
        Console.WriteLine($"Serialized: {json}");

        var deserialized = jsonSerializer.Deserialize(json);
        Console.WriteLine($"Deserialized: {deserialized}");
    }
}

public record Person(string Name, int Age);