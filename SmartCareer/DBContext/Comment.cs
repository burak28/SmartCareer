namespace SmartCareer.DBContext;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    public Guid Id { get; set; }
    public string Data { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Like { get; set; }
    public int Dislike { get; set; }
    public Guid UserId { get; set; }
    public string CommentId { get; set; }
}