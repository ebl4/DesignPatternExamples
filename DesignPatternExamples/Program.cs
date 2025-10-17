using DesignPatternExamples.ObjectPool;
using Microsoft.Extensions.DependencyInjection;

// ConexaoPool pool = new ConexaoPool();

// // Obter e usar conexões
// Conexao conexao1 = pool.ObterConexao();
// conexao1.Conectar();

// Conexao conexao2 = pool.ObterConexao();
// conexao2.Conectar();

// // Liberar conexões de volta ao pool
// conexao1.Desconectar();
// pool.LiberarConexao(conexao1);

// Conexao conexao3 = pool.ObterConexao();  // Reutilizando conexão liberada
// conexao3.Conectar();

// conexao2.Desconectar();
// pool.LiberarConexao(conexao2);

// // Testando padrão builder
// BuilderExample.Execute();

// // Testando padrão factory method
// FactoryExample.Execute();

// Testando padrão mediator
// var services = new ServiceCollection();
// services.AddUserManagement();

// var serviceProvider = services.BuildServiceProvider();

// var userService = serviceProvider.GetRequiredService<UserService>();

// try
// {
//     var createdUser = await userService.CreateUserAsync(
//         "john.doe@example.com",
//         "John Doe",
//         new DateTime(1990, 1, 1)
//     );

//     if (createdUser != null)
//     {
//         Console.WriteLine($"User created with ID: {createdUser.UserId}");

//         var user = await userService.GetUserAsync(createdUser.UserId);
//         if (user != null)
//         {
//             Console.WriteLine($"User retrieved: {user.Name}, Email: {user.Email}");
//         }
//         else
//         {
//             Console.WriteLine("User not found.");
//         }
//     }
//     else
//     {
//         Console.WriteLine("Failed to create user.");
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred: {ex.Message}");
// }

Console.WriteLine("\n=== Observer Pattern Example ===");
var orderService = new OrderService();

// Registrar observadores
orderService.Attach(new EmailNotificationService());
orderService.Attach(new InventoryService());
orderService.Attach(new AnalyticsService());

// Registrar evento
orderService.OrderStatusChanged += (sender, e) =>
{
    Console.WriteLine($"Event: Order {e.OrderId} changed to {e.Status} at {e.Timestamp}");
};

// Simular mudanças de status
orderService.UpdateOrderStatus("ORD-001", OrderStatus.Confirmed);
orderService.UpdateOrderStatus("ORD-001", OrderStatus.Shipped);

await Task.CompletedTask;
