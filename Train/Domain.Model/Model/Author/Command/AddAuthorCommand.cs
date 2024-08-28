using MediatR;

namespace Domain.Model.Model.Author.Command;

public class AddAuthorCommand(string name) : IRequest<bool>
{
    public string Name { get; set; } = name;
}