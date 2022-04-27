using FluentValidation;

namespace SmartCareer.Models;

public class UserCompleteRequest
{
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string[] Skills { get; set; }
}