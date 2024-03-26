using UnityEngine;

public class ErrorManager
{
    private static ErrorManager instance;

    private ErrorManager()
    {
    }

    public static ErrorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ErrorManager();
            }
            return instance;
        }
    }

    public string translateError(string error)
    {
        if (error.Equals("InvalidParams"))
        {
            return "Username and password invalid";
        }
        else if (error.Equals("EmailAddressNotAvailable"))
        {
            return "Email address not available";
        }
        return error;
    }

}
