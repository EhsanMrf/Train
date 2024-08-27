using Common.Response;
using Domain.Model.Model.Book.QueryModel;
using MediatR;

namespace Domain.Model.Model.Book.Query;

public class GetBookByIdQuery : IRequest<ServiceResponse<BookQueryModel?>>
{
    public Guid Id { get; set; }
}