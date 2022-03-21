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
        _dbContext.Users.Add(new User
        {
            MailAddress = userRequest.MailAddress,
            Password = userRequest.Password,
            Id = new Guid(),
            CreatedTime = DateTime.Now
        });

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> LoginAsync(UserRequest userRequest)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.MailAddress == userRequest.MailAddress && x.Password == userRequest.Password);

        if(user == null)
            return false;
        return true;
    }
}