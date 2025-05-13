namespace Shared.Exceptions;

public class LoginExistException : BaseApiException
{
    protected override string TitleMessage()
    {
        return "User with current Login exist";
    }

    protected override string DetailMessage()
    {
        return "User with current Login exist. Try Again";
    }

    protected override ErrorCode ApiCode()
    {
        return ErrorCode.UserAlreadyExists;
    }

    protected override int HttpStatusCode()
    {
        return 403;
    }
}