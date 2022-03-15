namespace SmartCareer.DBContext;

public class User
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public string MailAddress { get; set; }
    public string Password { get; set; }
}