

namespace RemoteMvpLib
{
    public enum ActionType
    {
       
        Register,
        Login,
        Logout  ,
        Delete,
        ShowUser
    }
    public enum UserType
    {
        None = 0,
        Admin,
        User
    }

    public class RemoteActionRequest
    {
        public ActionType Type { get; }
        public UserType UserType { get; }

        public string UserName { get; }

        public string Password { get; }

        public RemoteActionRequest(ActionType type, string username, string password,UserType userType)
        {
            Type = type;
            UserName = username;
            Password = password;
            UserType = userType;

        }
    }

    public enum ResponseType
    {
        Success,
        Error
    }

    public class RemoteActionResponse
    {
        public ResponseType Type { get; set; }

        public string? Message { get; set; }

        public RemoteActionResponse(ResponseType type, string? message)
        {
            Type = type;
            Message = message;
        }
    }
}
