using Common.MediatR;
using Common.Response;
using Domain.Model.Model.Author.QueryModel;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorByIdQuery:RequestMediator<Guid,ServiceResponse<AuthorQueryModel>>;