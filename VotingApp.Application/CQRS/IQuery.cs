using VotingApp.Domain.Abstraction;
using MediatR;

namespace VotingApp.Application.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }