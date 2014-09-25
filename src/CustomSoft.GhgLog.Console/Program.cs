namespace CustomSoft.GhgLog.Console
{
  using System;
  using System.Linq;
  using CustomSoft.GhgLog.Core.Extract;

  class Program
  {
    static void Main(string[] args)
    {
      var setting = CustomSoft.GhgLog.Console.Properties.Settings.Default;

      var e = new IssueExtractor(setting.GithubLogin, setting.GithubPassword);

      var data = e.Extract(new ExtractOptions()
        {
          Org = setting.GithubOwner,
          Repos = setting.GithubRepos
        });

      var t = new CustomSoft.GhgLog.Core.Transform.Changelog();
      t.Transform(data, new GhgLog.Core.Transform.Options()
        {
          CatchAllLabelName = setting.CatchAllLabelName,
          GroupByLabels = setting.GroupByLabels.Cast<String>(),
          ResultFile = setting.ResultFile,
          TemplateFile = setting.TemplateFile
        });
    }
  }
}
