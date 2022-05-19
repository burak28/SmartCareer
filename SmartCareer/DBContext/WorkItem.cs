using System.ComponentModel.DataAnnotations.Schema;

public class WorkItem
{
    public Guid Id { get; set; }
    public string Data { get; set; }
    public DateTime CreatedDate { get; set; }
    public string[] SkillSet { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
}