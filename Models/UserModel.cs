//File Path: /Models/UserModel.cs

public class User
{
    public int Id { get; set; }
    #pragma warning disable CS8618 
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string UserType { get; set; }
    
}