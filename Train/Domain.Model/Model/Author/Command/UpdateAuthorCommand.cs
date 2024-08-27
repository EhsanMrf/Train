using Common.Response;
using MediatR;

namespace Domain.Model.Model.Author.Command;

public class UpdateAuthorCommand : IRequest<ServiceResponse<Author>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}