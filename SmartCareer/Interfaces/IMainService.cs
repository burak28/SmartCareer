using SmartCareer.Models;
using Microsoft.AspNetCore.Mvc;
using SmartCareer.Models;
using SmartCareer.DBContext;

public interface IMainService
{
    public Task<List<CommentModel>> GetAllCommentsAsync(string userId, int skip, DateTime startDate);

    public Task<bool> AddCommentAsync(string userId, CommentRequest data);

    public Task<List<CommentModel>> GetSubCommentsAsync(string userId, string commentId);

    public Task<bool> CreateWorkItemAsync(string userId, WorkItemModel workItemModel);
    
    public Task<List<WorkItem>> GetWorkItemsAsync(string userId);

    public Task<WorkItem> GetWorkItemDetailAsync(string id, string userId);

    public Task<List<AIRequest>> UserAndJobsAsync(string id);

    public Task<List<string>> GraphicDataAsync(string id);
}