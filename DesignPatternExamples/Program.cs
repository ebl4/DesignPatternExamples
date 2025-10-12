using DesignPatternExamples.ObjectPool;

ConexaoPool pool = new ConexaoPool();

// Obter e usar conexões
Conexao conexao1 = pool.ObterConexao();
conexao1.Conectar();

Conexao conexao2 = pool.ObterConexao();
conexao2.Conectar();

// Liberar conexões de volta ao pool
conexao1.Desconectar();
pool.LiberarConexao(conexao1);

Conexao conexao3 = pool.ObterConexao();  // Reutilizando conexão liberada
conexao3.Conectar();

conexao2.Desconectar();
pool.LiberarConexao(conexao2);

// Testando padrão builder
BuilderExample.Execute();
