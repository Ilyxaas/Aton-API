namespace Shared.Exceptions;

public class ValidationException(string message = "") : BaseApiException
{

    public static string GenerateExcMessage(string argName)
    {
        return $"The {argName} contains more than just Latin letters and numbers.";
    }

    protected override string TitleMessage()
    {
        return "Input form is not Valid.";
    }

    protected override string DetailMessage()
    {
        return $"Input form is not Valid. {message}";
    }

    protected override ErrorCode ApiCode()
    {
        return ErrorCode.ValidationError;
    }

    protected override int HttpStatusCode()
    {
        return 400;
    }
    
    
}