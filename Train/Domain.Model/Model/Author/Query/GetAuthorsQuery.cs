using Common.Response;
using Domain.Model.Model.Author.QueryModel;
using MediatR;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorsQuery:IRequest<ServiceResponse<DataList<AuthorQueryModel>>>{}