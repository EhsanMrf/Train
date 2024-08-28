using Common.Response;
using MediatR;

namespace Domain.Model.Model.Author.Command;

public class UpdateAuthorCommand(Guid id, string name) : IRequest<ServiceResponse<Author>>
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
}