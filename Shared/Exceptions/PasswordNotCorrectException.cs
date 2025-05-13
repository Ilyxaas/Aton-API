namespace Shared.Exceptions;

public class PasswordNotCorrectException : BaseApiException
{
    protected override string TitleMessage()
    {
        return "Password is not valid.";
    }

    protected override string DetailMessage()
    {
        return "Password is not valid. Try again";
    }

    protected override ErrorCode ApiCode()
    {
        return ErrorCode.PasswordNotCorrect;
    }

    protected override int HttpStatusCode()
    {
        return 409;
    }
}