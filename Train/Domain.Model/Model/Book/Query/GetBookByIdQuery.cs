using Common.MediatR;
using Common.Response;

namespace Domain.Model.Model.Book.Query;

public class GetBookByIdQuery :RequestMediator<ServiceResponse<GetBooksQuery>>;