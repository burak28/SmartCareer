using Microsoft.AspNetCore.Mvc;
using SmartCareer.DBContext;
using SmartCareer.Models;

public class MainService : IMainService
{
    private readonly SmartCareerDBContext _dbContext;

    public MainService(SmartCareerDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    private async Task<bool> ControlUser(string userId)
    {
        var user = _dbContext.Users.First(x => x.Id.ToString() == userId);
        if (user == null)
            return false;
        return true;
    }

    public async Task<bool> AddCommentAsync(string userId, CommentRequest data)
    {
        var user = await ControlUser(userId);
        if (!user)
            return false;
        
        var comment = new Comment{
            Id = new Guid(),
            Data = data.Data,
            CreatedDate = DateTime.Now,
            Like = 0,
            Dislike = 0,
            UserId = Guid.Parse(userId),
            CommentId = data.CommentId
        };

        await _dbContext.Comments.AddAsync(comment);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CreateWorkItemAsync(string userId, WorkItemModel workItemModel)
    {
        var user = await ControlUser(userId);
        if (!user)
            return false;
        
        var item = new WorkItem{
            Id = new Guid(),
            Data = workItemModel.Data,
            CreatedDate = DateTime.Now,
            SkillSet = workItemModel.SkillSet,
            UserId = Guid.Parse(userId)
        };

        await _dbContext.WorkItems.AddAsync(item);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<CommentModel>> GetAllCommentsAsync(string userId, int skip, DateTime startDate)
    {
        var comments = _dbContext.Comments.OrderByDescending(x => x.CreatedDate).Skip(skip).Take(20).Select(x => new CommentModel{
            Data = x.Data,
            CreatedDate = x.CreatedDate,
            Id = x.Id,
            Like = x.Like,
            Dislike = x.Dislike,
            CommentId = x.CommentId
        }).ToList();

        if (comments.Count == 0)
            return new List<CommentModel>();
        return comments;
    }

    public async Task<List<CommentModel>> GetSubCommentsAsync(string userId, string commentId)
    {
        var user = await ControlUser(userId);
        if (!user)
            return null;
        
        var comments = _dbContext.Comments.OrderByDescending(x => x.CreatedDate).Where(x => x.CommentId == commentId).Select(x => new CommentModel{
            Data = x.Data,
            CreatedDate = x.CreatedDate,
            Id = x.Id,
            Like = x.Like,
            Dislike = x.Dislike,
            CommentId = commentId
        }).ToList();
        
        return comments;
    }
}