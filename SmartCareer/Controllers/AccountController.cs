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

    public AccountController(ILogger<AccountController> logger, SmartCareerDBContext dBContext)
    {
        _logger = logger;
        _dbContext = dBContext;
    }

    [HttpPut("register")]
    public ActionResult Register([FromBody] UserRequest userRequest)
    {
        _dbContext.Users.Add(new User
        {
            MailAddress = userRequest.MailAddress,
            Password = userRequest.Password,
            Id = new Guid(),
            CreatedTime = DateTime.Now
        });

        _dbContext.SaveChangesAsync();

        return Ok();
    }
}
