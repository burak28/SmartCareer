using Microsoft.AspNetCore.Mvc;
using SmartCareer.DBContext;
using SmartCareer.Models;

namespace SmartCareer.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{

    private readonly ILogger<AccountController> _logger;
    private readonly SmartCareerDBContext _dbContext;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, SmartCareerDBContext dBContext, IAccountService accountService)
    {
        _logger = logger;
        _dbContext = dBContext;
        _accountService = accountService;
    }

    [HttpPut("register")]
    public async Task<ActionResult> Register([FromBody] UserRequest userRequest)
    {
        return Ok(await _accountService.RegisterAsync(userRequest));
    }

    [HttpPut("login")]
    public async Task<ActionResult> Login([FromBody] UserRequest userRequest)
    {
        if(await _accountService.LoginAsync(userRequest))
            return Ok();
        else
            return Unauthorized();
    }
}
