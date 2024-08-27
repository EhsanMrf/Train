using Common.MediatR;
using Common.Response;

namespace Domain.Model.Model.Book.Query;

public class GetBookQuery :RequestMediator<ServiceResponse<DataList<GetBookQuery>>>;