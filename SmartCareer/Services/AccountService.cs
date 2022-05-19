using Microsoft.AspNetCore.Mvc;
using SmartCareer.DBContext;
using SmartCareer.Models;

public class AccountService : IAccountService
{
    private readonly SmartCareerDBContext _dbContext;

    public AccountService(SmartCareerDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<bool> RegisterAsync(UserRequest userRequest)
    {

        var user = _dbContext.Users.FirstOrDefault(x => x.MailAddress == userRequest.MailAddress);

        if(user != null)
            return false;

        var data = DateTime.UtcNow;
        var data2 = DateTime.UtcNow;

        _dbContext.Users.Add(new User
        {
            MailAddress = userRequest.MailAddress,
            Password = userRequest.Password,
            CreatedTime = DateTime.UtcNow,
            Id = new Guid(),
            IsProfileCompleted = false,
            PhoneNumber="",
            Username="",
            Skills = new string[] {},
        });

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<User> LoginAsync(UserRequest userRequest)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.MailAddress == userRequest.MailAddress && x.Password == userRequest.Password);

        if(user == null)
            return null;
        return user;
    }

    public async Task<User> CompleteRegisterAsync(UserCompleteRequest userCompleteRequest, string id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id.ToString() == id);

        if(user == null)
            return null;
        
        user.PhoneNumber = userCompleteRequest.PhoneNumber;
        user.Skills = userCompleteRequest.Skills;
        user.Username = userCompleteRequest.Username;

        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetProfileAsync(string id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id.ToString() == id);

        if(user == null)
            return null;

        return user;
    }
}