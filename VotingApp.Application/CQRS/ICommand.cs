using VotingApp.Domain.Abstraction;
using MediatR;

namespace VotingApp.Application.CQRS;

public interface ICommand : IRequest<Result>
{ }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{ }
