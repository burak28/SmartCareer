using FluentValidation;

namespace SmartCareer.Models;

public class WorkItemModel
{
    public string Data { get; set; }
    public string[] SkillSet { get; set; }
}

public class WorkItemModelValidator : AbstractValidator<WorkItemModel>
{

}