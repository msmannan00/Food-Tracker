public class userSessionManager : GenericSingletonClass<userSessionManager>
{
    public string mProfileUsername;
    public string mProfileID;

    public void OnInitialize(string pProfileUsername, string pProfileID)
    {
        this.mProfileUsername = pProfileUsername;
        this.mProfileID = pProfileID;
    }

    public void OnResetSession()
    {
        this.mProfileUsername = null;
        this.mProfileID = null;
    }

}

