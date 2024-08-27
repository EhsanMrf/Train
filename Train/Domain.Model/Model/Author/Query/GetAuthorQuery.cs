using Common.Response;
using Domain.Model.Model.Author.QueryModel;
using MediatR;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorByIdQuery : IRequest<ServiceResponse<AuthorQueryModel>>
{
    public Guid Id { get; set; }
}