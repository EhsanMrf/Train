using Common.Response;
using MediatR;

namespace Common.MediatR;

public abstract partial class RequestMediator<T> :IRequest<ServiceResponse>
{
    public T Id { get; set; }
}

public abstract class RequestMediator<T, TR> : IRequest<ServiceResponse<TR>> where T : struct where TR : ServiceResponse
{
    public T Id { get; set; }
}

public abstract partial class RequestMediator<T> : IRequest<ServiceResponse<T>>
{

}

public abstract class RequestMediator :IRequest;