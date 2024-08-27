using Application.Contract.Interface;
using Common.Response;
using Domain.Model.Model.Book;
using Domain.Model.Model.Book.Command;
using Domain.Model.Model.Book.IRepository;
using Domain.Model.Model.Book.Query;
using Domain.Model.Model.Book.QueryModel;

namespace Application.Service;

public class BookService : IBookService
{
    private readonly IBookCommandRepository _commandRepository;
    private readonly IBookQueryRepository _queryRepository;

    public BookService(IBookCommandRepository commandRepository, IBookQueryRepository queryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        await _commandRepository.Create(BookBuilder.Instance()
            .WithBookTitle(request.BookTitle)
            .WithPublishYear(request.PublishYear)
            .WithAuthorId(request.AuthorId)
            .Build());
    }

    public async Task<ServiceResponse<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _queryRepository.Load(request.Id);
        if (book==null)
            return new ServiceResponse<Book> {Message = "NotFound"};

        book.Update(request.BookTitle,request.PublishYear,request.AuthorId);
        await _commandRepository.Update(book);
        return new ServiceResponse<Book>(book);
    }


    public async Task<ServiceResponse<BookQueryModel?>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _queryRepository.GetById(request.Id);
        return book ;
    }

    public async Task<ServiceResponse<DataList<BookQueryModel>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _queryRepository.GetList();
        if (books==null|| !books.Any())
            return new ServiceResponse<DataList<BookQueryModel>>();

        return new ServiceResponse<DataList<BookQueryModel>>()
        {
            Data = new DataList<BookQueryModel>(books,books.Count(),1,int.MaxValue)
        };
    }
}