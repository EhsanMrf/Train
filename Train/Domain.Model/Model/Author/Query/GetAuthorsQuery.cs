using Common.Input;
using Common.Response;
using Domain.Model.Model.Author.QueryModel;
using MediatR;

namespace Domain.Model.Model.Author.Query;

public class GetAuthorsQuery:BaseInputRequest,IRequest<ServiceResponse<DataList<AuthorQueryModel>>>{}