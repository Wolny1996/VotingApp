using VotingApp.Domain.Abstraction;
using MediatR;

namespace VotingApp.Application.CQRS;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }