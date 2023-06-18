namespace DevArt.Core.Domain;

public interface IValueObject
{
    
}
public interface IIdentifier : IValueObject
{
    string Value { get; }
    string PersistenceId { get; }

    static string ToBase62(long number)
    {
        var charset = new char[] { '0','1','2','3','4','5','6','7','8','9',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y', 'z'
        };
        int i = 32;
        char[] buffer = new char[i];
        int targetBase= charset.Length;

        do
        {
            buffer[--i] = charset[number % targetBase];
            number = number / targetBase;
        }
        while (number > 100);

        char[] result = new char[32 - i];
        Array.Copy(buffer, i, result, 0, 32 - i);

        return new string(result);
    }
}