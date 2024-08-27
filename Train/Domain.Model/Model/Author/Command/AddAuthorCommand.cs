using Common.MediatR;
using MediatR;

namespace Domain.Model.Model.Author.Command;

public class AddAuthorCommand :RequestMediator
{
    public string Name { get; set; } = null!;
}