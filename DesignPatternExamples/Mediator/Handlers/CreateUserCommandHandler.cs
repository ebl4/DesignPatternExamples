using MediatR;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserCreatedResponse>
{
    public async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Simulação de criação de usuário
        await Task.Delay(500);
        var userId = Guid.NewGuid();
        
        return new UserCreatedResponse(
            userId, 
            request.Email, 
            DateTime.UtcNow
        );
    }
}