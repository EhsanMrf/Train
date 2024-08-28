using API.Controllers.ViewModel;
using Common.Response;
using Domain.Model.Model.Author;
using Domain.Model.Model.Author.Command;
using Domain.Model.Model.Author.Query;
using Domain.Model.Model.Author.QueryModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public async Task<bool> Create(SaveAuthorViewModel input)
    {
        return await mediator.Send(new AddAuthorCommand(input.Name));
    }

    [HttpPut("{id}")]
    public async Task<ServiceResponse<Author>> Update(Guid id,SaveAuthorViewModel input)
    {
        return await mediator.Send(new UpdateAuthorCommand(id,input.Name));
    }

    [HttpGet]
    public async Task<ServiceResponse<DataList<AuthorQueryModel>>> GetList()
    {
        return await mediator.Send(new GetAuthorsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ServiceResponse<AuthorQueryModel?>> Get(Guid id)
    {
        return await mediator.Send(new GetAuthorByIdQuery(id));
    }

    [HttpGet("AuthorBooks/{id}")]
    public async Task<ServiceResponse<DataList<AuthorBookQueryModel>>> GetAuthorBooks(Guid id)
    {
        return await mediator.Send(new GetAuthorBooksByAuthorId(id));
    }
}