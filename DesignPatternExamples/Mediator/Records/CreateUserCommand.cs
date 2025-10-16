using MediatR;

public record CreateUserCommand(string Email, string Name, DateTime BirthDate) : IRequest<UserCreatedResponse>;
