using Common.MediatR;
using Common.Response;
using Domain.Model.Model.Author.QueryModel;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorsQuery:RequestMediator<ServiceResponse<DataList<AuthorQueryModel>>>;