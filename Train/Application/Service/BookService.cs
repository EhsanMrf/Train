using Application.Contract.Interface;
using Application.Service.Exception;
using Common.Response;
using Domain.Model.Model.Author.IRepository;
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
    private readonly IAuthorQueryRepository _authorQueryRepository;

    public BookService(IBookCommandRepository commandRepository, IBookQueryRepository queryRepository, IAuthorQueryRepository authorQueryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
        _authorQueryRepository = authorQueryRepository;
    }

    public async Task<bool> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var authorId = await GetAuthorId(request.AuthorId);
        return await _commandRepository.Create(BookBuilder.Instance()
            .WithBookTitle(BookTitle.CreateInstance(request.Title))
            .WithPublishYear(request.PublishYear)
            .WithAuthorId(authorId ?? Guid.Empty)
            .Build());
    }

    public async Task<ServiceResponse<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _queryRepository.Load(request.Id);
        book.ReturnDataOrThrow(new BookNotFoundServiceException());
        book!.Update(request.Title,request.PublishYear,request.AuthorId);
        await _commandRepository.Update(book);
        return new ServiceResponse<Book>(book);
    }


    public async Task<ServiceResponse<BookQueryModel?>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _queryRepository.GetById(request.Id);
        return book.ReturnDataOrInstance();
    }

    public async Task<ServiceResponse<DataList<BookQueryModel>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _queryRepository.GetList(request.DataRequest);
        return books.ReturnDataOrInstance();
    }

    private async Task<Guid?> GetAuthorId(Guid authorId)
    {
        return await _authorQueryRepository.GetAuthorId(authorId);
    }
}