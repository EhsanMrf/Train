using Common.Response;
using Domain.Model.Model.Author.QueryModel;
using MediatR;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorBooksByAuthorId : IRequest<ServiceResponse<DataList<AuthorBookQueryModel>>>
{
    public Guid Id { get; set; }
}