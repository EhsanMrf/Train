using Application.Contract.Interface;
using Common.Response;
using Domain.Model.Model.Author;
using Domain.Model.Model.Author.Command;
using Domain.Model.Model.Author.IRepository;
using Domain.Model.Model.Author.Query;
using Domain.Model.Model.Author.QueryModel;

namespace Application.Service;

public class AuthorService(IAuthorQueryRepository queryRepository, IAuthorCommandRepository commandRepository)
    : IAuthorService
{
    public async Task<bool> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
       return await commandRepository.Create(new Author(request.Name));
    }

    public async Task<ServiceResponse<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await queryRepository.Load(request.Id);
        if (author==null)
            return new ServiceResponse<Author> { Message = "NotFound" };

        author.Update(request.Name);
        await commandRepository.Update(author);
        return new ServiceResponse<Author>(author);
    }

    public async Task<ServiceResponse<AuthorQueryModel?>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await queryRepository.GetById(request.Id);
        return author;
    }

    public async Task<ServiceResponse<DataList<AuthorQueryModel>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
         var authors = await queryRepository.GetList();
         if (authors == null || !authors.Any())
             return new ServiceResponse<DataList<AuthorQueryModel>>();

         return new ServiceResponse<DataList<AuthorQueryModel>>()
         {
             Data = new DataList<AuthorQueryModel>(authors, authors.Count(), 1, int.MaxValue)
         };
    }

    public async Task<ServiceResponse<DataList<AuthorBookQueryModel>>> Handle(GetAuthorBooksByAuthorId request, CancellationToken cancellationToken)
    {
        var authors = await queryRepository.GetAuthorBooksById(request.Id);
        if (authors == null || !authors.Any())
            return new ServiceResponse<DataList<AuthorBookQueryModel>>();

        return new ServiceResponse<DataList<AuthorBookQueryModel>>()
        {
            Data = new DataList<AuthorBookQueryModel>(authors, authors.Count(), 1, int.MaxValue)
        };
    }
}