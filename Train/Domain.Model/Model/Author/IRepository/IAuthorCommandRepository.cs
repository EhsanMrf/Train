using Common.Response;
using Common.TransientService;

namespace Domain.Model.Model.Author.IRepository;

public interface IAuthorCommandRepository :ITransientService
{
    Task Create(Author command);
    Task<Author> Update(Author command);
}