using SmartCareer.Models;
using Microsoft.AspNetCore.Mvc;
using SmartCareer.Models;
using SmartCareer.DBContext;

public interface IAccountService
{
    public Task<bool> RegisterAsync(UserRequest userRequest);

    public Task<User> LoginAsync(UserRequest userRequest);

    public Task<User> CompleteRegisterAsync(UserCompleteRequest userCompleteRequest, string id);
}