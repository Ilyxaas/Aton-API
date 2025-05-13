namespace Shared.Exceptions;

public class UserNotFoundException : BaseApiException
{
    protected override string TitleMessage()
        {
            return "User not Found.";
        }
    
        protected override string DetailMessage()
        {
            return "User not Found. Try Again";
        }
    
        protected override ErrorCode ApiCode()
        {
            return ErrorCode.UserNotFound;
        }
    
        protected override int HttpStatusCode()
        {
            return 403;
        }
}