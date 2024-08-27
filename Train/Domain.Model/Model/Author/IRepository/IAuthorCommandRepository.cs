using Common.Response;
using Domain.Model.Model.Author.Command;

namespace Domain.Model.Model.Author.IRepository;

public interface IAuthorCommandRepository
{
    Task Create(AddAuthorCommand command);
    Task<ServiceResponse<Author>> Update(UpdateAuthorCommand command);
}