namespace DesignPatternExamples.ObjectPool
{
    // Classe de exemplo que será gerenciada pelo Object Pool
    public class Conexao
    {
        public string Id { get; set; }

        public Conexao(string id)
        {
            Id = id;
            Console.WriteLine($"Conexão {Id} criada.");
        }

        public void Conectar()
        {
            Console.WriteLine($"Conexão {Id} conectada.");
        }

        public void Desconectar()
        {
            Console.WriteLine($"Conexão {Id} desconectada.");
        }
    }
}
