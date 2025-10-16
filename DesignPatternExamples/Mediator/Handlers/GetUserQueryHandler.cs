using MediatR;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        // Simulação de busca
        await Task.Delay(200);
        
        return new UserResponse(
            request.UserId,
            "user@example.com",
            "John Doe",
            30
        );
    }
}