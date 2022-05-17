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
}