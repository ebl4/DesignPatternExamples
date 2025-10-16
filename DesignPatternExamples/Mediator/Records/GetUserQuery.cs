using MediatR;

public record GetUserQuery(Guid UserId) : IRequest<UserResponse>;
