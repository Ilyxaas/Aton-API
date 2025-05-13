using System.Net;
using Shared.Exceptions;

namespace AtonWebAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await _next(httpContent);
            }
            catch (DontHavePermissionException ex)
            {
                await HandleExceptionAsync(httpContent, ex.Message,
                    HttpStatusCode.BadRequest, ex.CreateResponse(httpContent));
            }
            catch (LoginExistException ex)
            {
                await HandleExceptionAsync(httpContent, ex.Message,
                    HttpStatusCode.BadRequest, ex.CreateResponse(httpContent));
            }
            catch (PasswordNotCorrectException ex)
            {
                await HandleExceptionAsync(httpContent, ex.Message,
                    HttpStatusCode.BadRequest, ex.CreateResponse(httpContent));
            }
            catch (UserNotFoundException ex)
            {
                await HandleExceptionAsync(httpContent, ex.Message,
                    HttpStatusCode.BadRequest, ex.CreateResponse(httpContent));
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContent, ex.Message,
                    HttpStatusCode.BadRequest, ex.CreateResponse(httpContent));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContent, $"{ex.Message} \n {ex.StackTrace} \n {ex.TargetSite}"
                    , HttpStatusCode.BadRequest, ex.CreateResponse(httpContent));
            }
        }
    
        private async Task HandleExceptionAsync(
            HttpContext context,
            string exMsg,
            HttpStatusCode code,
            ApiErrorResponse apiErrorResponse) 
        {
            _logger.LogError(exMsg);
    
            HttpResponse httpResponse = context.Response;
    
            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = (int)code;
    
            await httpResponse.WriteAsync(apiErrorResponse.ToJson());
    
        }
}