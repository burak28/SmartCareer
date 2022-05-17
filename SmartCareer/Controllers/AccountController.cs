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

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserRequest userRequest)
    {
        if (await _accountService.RegisterAsync(userRequest))
            return Ok();
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserRequest userRequest)
    {
        var user = await _accountService.LoginAsync(userRequest);

        if(user != null)
            return Ok(user);
        else
            return Unauthorized();
    }

    [HttpPost("completeregister")]
    public async Task<ActionResult> CompleteRegister([FromBody] UserCompleteRequest userCompleteRequest, [FromHeader] string userid)
    {
        var user = await _accountService.CompleteRegisterAsync(userCompleteRequest, userid);

        if(user != null)
            return Ok(user);
        else
            return Unauthorized();
    }

    [HttpGet("profile/{id}")]
    public async Task<ActionResult> GetProfile(string id)
    {
        var user = await _accountService.GetProfileAsync(id);

        if(user != null)
            return Ok(user);
        else
            return NotFound();
    }
}
