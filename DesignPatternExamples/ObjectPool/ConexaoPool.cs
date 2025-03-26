namespace DesignPatternExamples.ObjectPool
{
    // Classe Object Pool para gerenciar as conexões
    public class ConexaoPool
    {
        private List<Conexao> _disponiveis = new List<Conexao>();
        private List<Conexao> _emUso = new List<Conexao>();

        public Conexao ObterConexao()
        {
            Conexao conexao;

            if (_disponiveis.Count > 0)
            {
                conexao = _disponiveis[0];
                _disponiveis.RemoveAt(0);
                Console.WriteLine($"Conexão {conexao.Id} reutilizada.");
            }
            else
            {
                conexao = new Conexao(Guid.NewGuid().ToString());
            }

            _emUso.Add(conexao);
            return conexao;
        }

        public void LiberarConexao(Conexao conexao)
        {
            _emUso.Remove(conexao);
            _disponiveis.Add(conexao);
            Console.WriteLine($"Conexão {conexao.Id} liberada e retornada ao pool.");
        }
    }
}
