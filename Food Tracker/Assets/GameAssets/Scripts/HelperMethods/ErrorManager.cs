using UnityEngine;

public class ErrorManager : GenericSingletonClass<ErrorManager>
{
    public string getTranslateError(string pError)
    {
        if (pError.Equals("InvalidParams"))
        {
            return "Username and password invalid";
        }
        else if (pError.Equals("EmailAddressNotAvailable"))
        {
            return "Email address not available";
        }
        return pError;
    }

}
