namespace CustomSoft.GhgLog.Core.Transform
{
  using System;
  using System.Collections.Generic;

  public class Options
  {
    public IEnumerable<String> GroupByLabels { get; set; }

    public String CatchAllLabelName { get; set; }

    public String ResultFile { get; set; }

    public String TemplateFile { get; set; }
  }
}
