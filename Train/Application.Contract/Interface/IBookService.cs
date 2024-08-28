using Common.Response;
using Common.TransientService;
using Domain.Model.Model.Book;
using Domain.Model.Model.Book.Command;
using Domain.Model.Model.Book.Query;
using Domain.Model.Model.Book.QueryModel;
using MediatR;

namespace Application.Contract.Interface;

public interface IBookService :  
    IRequestHandler<AddBookCommand,bool>,
    IRequestHandler<UpdateBookCommand, ServiceResponse<Book>>,
    IRequestHandler<GetBookByIdQuery,ServiceResponse<BookQueryModel?>>,
    IRequestHandler<GetBooksQuery, ServiceResponse<DataList<BookQueryModel>>>,
    ITransientService;