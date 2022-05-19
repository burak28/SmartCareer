namespace SmartCareer.DBContext;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public string Username { get; set; }
    public string MailAddress { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string[] Skills { get; set; }
    public bool IsProfileCompleted { get; set; }
}