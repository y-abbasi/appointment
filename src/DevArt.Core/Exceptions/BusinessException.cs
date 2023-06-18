namespace DevArt.Core.ErrorHandling;

public class BusinessException : Exception
{
    public string Code { get; }

    public BusinessException(string code, string message) : base(message)
    {
        Code = code;
    }
}