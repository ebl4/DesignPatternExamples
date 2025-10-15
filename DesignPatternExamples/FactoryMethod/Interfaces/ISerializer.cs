// Interface base para serializadores
public interface ISerializer<T>
{
    string Serialize(T obj);
    T Deserialize(string data);
}