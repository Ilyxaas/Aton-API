namespace Shared.Exceptions;

public enum ErrorCode
{
    UndetectedError = 1000,
        
    DontHavePermission = 1002,
        
    ValidationError = 1003,
        
    UserAlreadyExists = 1004,
        
    UserNotFound = 1005,
        
    PasswordNotCorrect = 1008
}