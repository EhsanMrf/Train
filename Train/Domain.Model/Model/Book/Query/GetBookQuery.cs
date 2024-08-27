using Common.MediatR;
using Common.Response;

namespace Domain.Model.Model.Book.Query;

public class GetBooksQuery :RequestMediator<ServiceResponse<DataList<GetBooksQuery>>>;