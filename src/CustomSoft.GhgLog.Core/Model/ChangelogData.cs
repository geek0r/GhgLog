namespace CustomSoft.GhgLog.Core.Model
{
  using System;
  using System.Collections.Generic;

  public class ChangelogData
  {
    public String Repository { get; set; }

    public String Owner { get; set; }

    public IEnumerable<Issue> Issues { get; set; }
  }
}
