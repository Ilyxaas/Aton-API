using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Shared.Exceptions;

public static class ApiErrorResponseExtension
{
    public static string ToJson(this ApiErrorResponse apiErrorResponse)
        {
            return JsonSerializer.Serialize(apiErrorResponse, new JsonSerializerOptions()
            {
                WriteIndented = true 
            });
        }
    
        public static ApiErrorResponse CreateResponse(
            this Exception ex,
            HttpContext context,
            IDictionary<string, string[]>? value = null)
        {
            var request = context.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

            return new ApiErrorResponse(
                title: "не обнаруженная ошибка",
                status: 500,
                instance: url,
                detail: "Обратитесь в службу поддержки или пожалуйста, попробуйте позже.",
                errorCode: ErrorCode.UndetectedError,
                anyValue: null);
        }
}