using Microsoft.AspNetCore.Http;

namespace Shared.Exceptions;

public abstract class BaseApiException : Exception
{
    protected abstract string TitleMessage();

    protected abstract string DetailMessage();

    protected abstract ErrorCode ApiCode();

    protected abstract int HttpStatusCode();
    
    public ApiErrorResponse CreateResponse(HttpContext context, Dictionary<string, string[]>? value = null)
    {
        var request = context.Request;
        var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        
        return new ApiErrorResponse(
            title: TitleMessage(),
            status: HttpStatusCode(),
            detail: DetailMessage(),
            instance: url,
            errorCode: ApiCode(),
            anyValue: value);
    }

}

