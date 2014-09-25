namespace CustomSoft.GhgLog.Core.Transform
{
  using System;
  using System.Collections.Generic;

  public class TransformedRepresentation
  {
    public IEnumerable<CustomSoft.GhgLog.Core.Model.Issue> Issues { get; set; }

    public IEnumerable<Milestone> Milestones { get; set; }

    public DateTime BuildDate { get; set; }

    public String Repository { get; set; }

    public String Owner { get; set; }
  }

  public class Milestone
  {
    public String Title { get; set; }

    public DateTime? ClosedOn { get; set; }

    public IEnumerable<Label> Labels { get; set; }

    public IEnumerable<CustomSoft.GhgLog.Core.Model.Issue> Issues { get; set; }
  }

  public class Label
  {
    public String Title { get; set; }

    public String Color { get; set; }

    public IEnumerable<CustomSoft.GhgLog.Core.Model.Issue> Issues { get; set; }
  }
}
