namespace Shared.Exceptions;

public class DontHavePermissionException : BaseApiException
{
    protected override string TitleMessage()
    {
        return "Don't have permission";
    }

    protected override string DetailMessage()
    {
        return "Don't have permission. Try again";
    }

    protected override ErrorCode ApiCode()
    {
        return ErrorCode.DontHavePermission;
    }

    protected override int HttpStatusCode()
    {
        return 403;
    }
}