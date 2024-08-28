using API.Controllers.ViewModel;
using Application.Contract.Interface;
using Common.Response;
using Domain.Model.Model.Author.Command;
using Domain.Model.Model.Book;
using Domain.Model.Model.Book.Command;
using Domain.Model.Model.Book.Query;
using Domain.Model.Model.Book.QueryModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<bool> Create(SaveBookViewModel input)
        {
           return await mediator.Send(new AddBookCommand(input.Title, input.PublishYear, input.AuthorId));
        }

        [HttpPut("{id}")]
        public async Task<ServiceResponse<Book>> Update(Guid id,SaveBookViewModel input)
        {
            return await mediator.Send(new UpdateBookCommand(id, input.Title, input.PublishYear, input.AuthorId));
        }

        [HttpGet]
        public async Task<ServiceResponse<DataList<BookQueryModel>>> GetList()
        {
            return await mediator.Send(new GetBooksQuery());
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse<BookQueryModel?>> Get(Guid id)
        {
            return await mediator.Send(new GetBookByIdQuery(id));
        }
    }
}
