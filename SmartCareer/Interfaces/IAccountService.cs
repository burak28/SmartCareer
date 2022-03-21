using SmartCareer.Models;
using Microsoft.AspNetCore.Mvc;

public interface IAccountService
{
    public Task<bool> RegisterAsync(UserRequest userRequest);

    public Task<bool> LoginAsync(UserRequest userRequest);
}