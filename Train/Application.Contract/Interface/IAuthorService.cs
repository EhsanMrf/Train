using Common.Response;
using Common.TransientService;
using Domain.Model.Model.Author;
using Domain.Model.Model.Author.Command;
using Domain.Model.Model.Author.Query;
using Domain.Model.Model.Author.QueryModel;
using MediatR;

namespace Application.Contract.Interface;

public interface IAuthorService :
    IRequestHandler<AddAuthorCommand,bool>,
    IRequestHandler<UpdateAuthorCommand, ServiceResponse<Author>>,
    IRequestHandler<GetAuthorByIdQuery, ServiceResponse<AuthorQueryModel?>>,
    IRequestHandler<GetAuthorsQuery, ServiceResponse<DataList<AuthorQueryModel>>>,
    IRequestHandler<GetAuthorBooksByAuthorId, ServiceResponse<DataList<AuthorBookQueryModel>>>,
    ITransientService;