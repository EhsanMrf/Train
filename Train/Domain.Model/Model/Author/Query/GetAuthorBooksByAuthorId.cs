using Common.Input;
using Common.Response;
using Domain.Model.Model.Author.QueryModel;
using MediatR;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorBooksByAuthorId(Guid id) :BaseInputRequest, IRequest<ServiceResponse<DataList<AuthorBookQueryModel>>>
{
    public Guid Id { get; set; } = id;
}