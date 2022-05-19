using Microsoft.AspNetCore.Mvc;
using SmartCareer.DBContext;
using SmartCareer.Models;
using Newtonsoft.Json;

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

        var comment = new Comment
        {
            Id = new Guid(),
            Data = data.Data,
            CreatedDate = DateTime.UtcNow,
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
        var control = await ControlUser(userId);
        if (!control)
            return false;

        var user = _dbContext.Users.Where(x => x.Id.ToString() == userId).FirstOrDefault();

        var item = new WorkItem
        {
            Id = new Guid(),
            Data = workItemModel.Data,
            CreatedDate = DateTime.UtcNow,
            SkillSet = workItemModel.SkillSet,
            UserId = Guid.Parse(userId),
            UserEmail = user.MailAddress,
            UserName = user.Username
        };

        await _dbContext.WorkItems.AddAsync(item);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<CommentModel>> GetAllCommentsAsync(string userId, int skip, DateTime startDate)
    {
        var comments = _dbContext.Comments.OrderByDescending(x => x.CreatedDate).Skip(skip).Select(x => new CommentModel
        {
            Data = x.Data,
            CreatedDate = x.CreatedDate,
            Id = x.Id,
            Like = x.Like,
            Dislike = x.Dislike,
            CommentId = x.CommentId,
            UserId = x.UserId
        }).ToList();

        foreach (var item in comments)
        {
            var user = _dbContext.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
            item.UserEmail = user.MailAddress;
        }

        if (comments.Count == 0)
            return new List<CommentModel>();
        return comments;
    }

    public async Task<List<CommentModel>> GetSubCommentsAsync(string userId, string commentId)
    {
        var user = await ControlUser(userId);
        if (!user)
            return null;

        var comments = _dbContext.Comments.OrderByDescending(x => x.CreatedDate).Where(x => x.CommentId == commentId).Select(x => new CommentModel
        {
            Data = x.Data,
            CreatedDate = x.CreatedDate,
            Id = x.Id,
            Like = x.Like,
            Dislike = x.Dislike,
            CommentId = commentId
        }).ToList();

        return comments;
    }

    public async Task<List<WorkItem>> GetWorkItemsAsync(string userId)
    {
        var user = await ControlUser(userId);
        if (!user)
            return null;

        var items = _dbContext.WorkItems.Where(x => x.UserId.ToString() == userId).ToList();

        return items;
    }

    public async Task<WorkItem> GetWorkItemDetailAsync(string id, string userId)
    {
        var user = await ControlUser(userId);
        if (!user)
            return null;

        var items = _dbContext.WorkItems.Where(x => x.Id.ToString() == id).FirstOrDefault();

        return items;
    }

    public async Task<List<AIRequest>> UserAndJobsAsync(string id)
    {
        var control = await ControlUser(id);
        if (!control)
            return null;

        var items = _dbContext.WorkItems.Where(x => x.UserId.ToString() != id).Select(x => new AIRequest
        {
            Id = x.Id.ToString(),
            Skills = x.SkillSet
        }).ToList();

        var user = _dbContext.Users.Where(x => x.Id.ToString() == id).Select(x => new AIRequest
        {
            Id = x.Id.ToString(),
            Skills = x.Skills
        }).FirstOrDefault();

        items.Add(user);

        return items;
    }

    public async Task<List<string>> GraphicDataAsync(string id)
    {
        var control = await ControlUser(id);
        if (!control)
            return null;

        var result = new GraphicModel
        {
            MaxValues = new List<Dictionary<string, int>>(),
            MinValues = new List<Dictionary<string, int>>()
        };

        var items = _dbContext.WorkItems.ToList();

        int i = 0;

        var nowTime = DateTime.UtcNow;

        while (i < 6)
        {
            Dictionary<string, int> mp = new Dictionary<string, int>();

            var MonthItems = items.Where(x => x.CreatedDate.Month == nowTime.Month && x.CreatedDate.Year == nowTime.Year).ToList();

            if (MonthItems.Count != 0)
            {
                foreach (var item in MonthItems)
                {
                    var deneme = countFreq(item.SkillSet, item.SkillSet.Length, mp);
                }
            }


            i = i + 1;
            nowTime = nowTime.AddMonths(-1);
            if (i == 1)
            {
                var maxValueKeys = mp.OrderByDescending(x => x.Value).Take(3).ToDictionary(e => e.Key,e => e.Value);
                maxValueKeys.Add(nowTime.Year.ToString()+"-"+nowTime.Month.ToString(), -1);
                var minValueKeys = mp.OrderBy(x => x.Value).Take(3).ToDictionary(e => e.Key,e => e.Value);
                minValueKeys.Add(nowTime.Year.ToString()+"-"+nowTime.Month.ToString(), -1);

                result.MaxValues.Add(maxValueKeys);
                result.MinValues.Add(minValueKeys);
            }
            else
            {
                var maxValue = mp.Where(x => result.MaxValues[0].Keys.ToArray().Contains(x.Key)).ToDictionary(e => e.Key,e => e.Value);
                maxValue.Add(nowTime.Year.ToString()+"-"+nowTime.Month.ToString(), -1);
                var minValue = mp.Where(x => result.MinValues[0].Keys.ToArray().Contains(x.Key)).ToDictionary(e => e.Key,e => e.Value);
                minValue.Add(nowTime.Year.ToString()+"-"+nowTime.Month.ToString(), -1);

                result.MaxValues.Add(maxValue);
                result.MinValues.Add(minValue);
            }
        }

        var result2 = new List<string>();

        result2.Add(JsonConvert.SerializeObject(result.MaxValues));
        result2.Add(JsonConvert.SerializeObject(result.MinValues));


        return result2;
    }

    private Dictionary<string, int> countFreq(string[] arr, int n, Dictionary<string, int> mp)
    {


        for (int i = 0; i < n; i++)
        {
            if (mp.ContainsKey(arr[i]))
            {
                var val = mp[arr[i]];
                mp.Remove(arr[i]);
                mp.Add(arr[i], val + 1);
            }
            else
            {
                mp.Add(arr[i], 1);
            }
        }

        return mp;
    }
}