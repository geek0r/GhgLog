namespace CustomSoft.GhgLog.Core.Transform
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using RazorEngine;

  /// <summary>
  /// The transformer creating an optimized represention of the issues used
  /// by the template to render it.
  /// </summary>
  public class Changelog
  {
    public void Transform(IEnumerable<CustomSoft.GhgLog.Core.Model.Issue> data, Options opt)
    {
      var sb = new StringBuilder();

      var changeTpl = File.ReadAllText(opt.TemplateFile);
      var represData = new CustomSoft.GhgLog.Core.Transform.TransformedRepresentation()
      {
        BuildDate = DateTime.Now,
        Issues = data
      };

      // Fetch milestones and labels
      var milestones = new List<CustomSoft.GhgLog.Core.Transform.Milestone>();

      // Eval each milestone
      foreach (var grp in data.GroupBy(x => x.Milestone).OrderByDescending(x => x.Key))
      {
        var labels = new List<CustomSoft.GhgLog.Core.Transform.Label>();

        var tmp = grp.FirstOrDefault();
        var milestone = new CustomSoft.GhgLog.Core.Transform.Milestone()
        {
          Title = tmp.Milestone,
          ClosedOn = tmp.MilestoneDate,
          Issues = data.Where(x => x.Milestone == grp.Key)
        };

        // Get all labels "within" that milestone 
        var uniqueLables = new List<String>();
        foreach (var lbls in grp.Select(x => x.Labels.ToList()))
        {
          uniqueLables = uniqueLables.Union(lbls).ToList();
        }
        var labelsToGroupBy = uniqueLables.Intersect(opt.GroupByLabels);

        // Fetch each issue list for each label
        var labelsInMilestone = new List<CustomSoft.GhgLog.Core.Transform.Label>();
        foreach (var lbl in labelsToGroupBy)
        {
          labelsInMilestone.Add(new GhgLog.Core.Transform.Label()
            {
              Title = lbl,
              Issues = grp.Where(x => x.Labels.Contains(lbl))
            });
        }

        // Catch all label
        var catchAllLabels = uniqueLables.Except(labelsToGroupBy);

        if (catchAllLabels.Count() > 0)
        {
          labelsInMilestone.Add(new GhgLog.Core.Transform.Label()
          {
            Title = opt.CatchAllLabelName,
            Issues = grp.Where(x => x.Labels.Intersect(catchAllLabels).Any())
          });
        }

        milestone.Labels = labelsInMilestone.OrderBy(x => x.Title).ToList();
        milestones.Add(milestone);
      }

      represData.Milestones = milestones.OrderByDescending(x => x.Title).ToList();

      var tplData = Razor.Parse(changeTpl, represData);

      if (File.Exists(opt.ResultFile))
      {
        File.Delete(opt.ResultFile);
      }

      File.AppendAllText(opt.ResultFile, tplData, System.Text.Encoding.UTF8);
    }
  }
}
