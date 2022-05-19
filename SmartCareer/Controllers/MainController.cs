using Microsoft.AspNetCore.Mvc;
using SmartCareer.DBContext;
using SmartCareer.Models;

namespace SmartCareer.Controllers;

[ApiController]
[Route("api/main")]
public class MainController : ControllerBase
{

    private readonly ILogger<AccountController> _logger;
    private readonly SmartCareerDBContext _dbContext;
    private readonly IAccountService _accountService;
    private readonly IMainService _mainService;

    public MainController(ILogger<AccountController> logger, SmartCareerDBContext dBContext, IAccountService accountService, IMainService mainService)
    {
        _logger = logger;
        _dbContext = dBContext;
        _accountService = accountService;
        _mainService = mainService;
    }

    [HttpGet("allcomments")]
    public async Task<ActionResult> AllComments([FromHeader] string userId, [FromHeader] int skip, [FromHeader] DateTime startDate)
    {
        var items = await _mainService.GetAllCommentsAsync(userId, skip, startDate);
        if (items != null)
            return Ok(items);
        return BadRequest();
    }

    [HttpGet("subcomments")]
    public async Task<ActionResult> SubComments([FromHeader] string userId, [FromHeader] string commentId)
    {
        var items = await _mainService.GetSubCommentsAsync(userId, commentId);
        if (items != null)
            return Ok(items);
        return BadRequest();
    }

    [HttpPost("comment")]
    public async Task<ActionResult> AddComment([FromHeader] string userId, [FromBody] CommentRequest data)
    {
        var items = await _mainService.AddCommentAsync(userId, data);
        if (items)
            return Ok();
        return BadRequest();
    }

    [HttpPost("workitem")]
    public async Task<ActionResult> CreateWorkItem([FromHeader] string userId, [FromBody] WorkItemModel workItemModel)
    {
        var items = await _mainService.CreateWorkItemAsync(userId, workItemModel);
        if (items)
            return Ok();
        return BadRequest();
    }

    [HttpGet("workitem")]
    public async Task<ActionResult> GetWorkItems([FromHeader] string userId)
    {
        var items = await _mainService.GetWorkItemsAsync(userId);
        if (items.Count != 0)
            return Ok(items);
        return BadRequest();
    }

    [HttpGet("workitem/{id}")]
    public async Task<ActionResult> GetWorkItemDetail(string id, [FromHeader] string userId)
    {
        var items = await _mainService.GetWorkItemDetailAsync(id, userId);
        if (items != null)
            return Ok(items);
        return BadRequest();
    }

    [HttpGet("userandjobs/{id}")]
    public async Task<ActionResult> UserAndJobs(string id)
    {
        var items = await _mainService.UserAndJobsAsync(id);
        if (items.Count != 0)
            return Ok(items);
        return BadRequest();
    }

    [HttpGet("graphicdata/{id}")]
    public async Task<ActionResult> GraphicData(string id)
    {
        var items = await _mainService.GraphicDataAsync(id);
        if (items != null)
            return Ok(items);
        return BadRequest();
    }
}
