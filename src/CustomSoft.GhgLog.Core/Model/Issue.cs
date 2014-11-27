namespace CustomSoft.GhgLog.Core.Model
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Internal representation of the Github issue
  /// </summary>
  public class Issue
  {
    public Int64 Id { get; set; }

    public String Title { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public String Assignee { get; set; }

    public String Creator { get; set; }

    public String Body { get; set; }

    public String Milestone { get; set; }

    public DateTime? MilestoneDate { get; set; }

    public DateTime? MilestoneCloseDate { get; set; }

    public IEnumerable<String> Labels { get; set; }

    public IEnumerable<String> Comments { get; set; }

    public String Url { get; set; }
  }
}
