using Common.TransientService;

namespace Domain.Model.Model.Author.IRepository;

public interface IAuthorCommandRepository :ITransientService
{
    Task<bool> Create(Author command);
    Task<Author> Update(Author command);
}