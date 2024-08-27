using MediatR;

namespace Domain.Model.Model.Author.Command;

public class AddAuthorCommand :IRequest
{
    public string Name { get; set; } = null!;
}