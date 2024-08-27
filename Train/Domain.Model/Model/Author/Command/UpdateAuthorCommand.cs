using Common.MediatR;
using Common.Response;

namespace Domain.Model.Model.Author.Command;

public class UpdateAuthorCommand : RequestMediator<Guid,ServiceResponse<Author>>
{
    public string Name { get; set; } = null!;
}