namespace CustomSoft.GhgLog.Core.Transform
{
  using System;
  using System.Linq;

  internal static class Mapping
  {
    public static CustomSoft.GhgLog.Core.Model.Issue ToIssue(this Octokit.Issue data)
    {
      var i = new Model.Issue()
      {
        Id = data.Number,
        Title = data.Title,
        Body = data.Body,
        Created = data.CreatedAt.DateTime,
        Updated = data.UpdatedAt.HasValue ? data.UpdatedAt.Value.DateTime : new System.Nullable<DateTime>(),
        Labels = data.Labels.Select(x => x.Name).ToList(),
        Url = data.HtmlUrl.AbsoluteUri
      };

      if(data.Assignee != null)
      {
        i.Assignee = data.Assignee.Name;
      }

      if(data.Milestone != null)
      {
        i.Milestone = data.Milestone.Title;
        i.MilestoneDate = data.Milestone.DueOn.HasValue ? data.Milestone.DueOn.Value.DateTime : new System.Nullable<DateTime>();
      };

      return i;
    }
  }
}
