using MediatR;

public class UserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<UserCreatedResponse> CreateUserAsync(string email, string name, DateTime birthDate)
    {
        var command = new CreateUserCommand(email, name, birthDate);
        return await _mediator.Send(command);
    }

    public async Task<UserResponse> GetUserAsync(Guid userId)
    {
        var query = new GetUserQuery(userId);
        return await _mediator.Send(query);
    }
}