namespace CustomSoft.GhgLog.Core.Extract
{
  using System.Collections.Generic;
  using System.Linq;
  using Octokit;
  using Transform;

  /// <summary>
  /// Extract the issues from Github in order to create the changelog
  /// </summary>
  public class IssueExtractor
  {
    #region Members
    public GitHubClient Client { get; private set; }
    #endregion

    #region Ctor
    public IssueExtractor(string login, string password)
    {
      var cred = new Credentials(login, password);
      this.Client = new GitHubClient(new ProductHeaderValue("CustomSoft.GhgLog"));
      this.Client.Credentials = cred;
    }
    #endregion

    public CustomSoft.GhgLog.Core.Model.ChangelogData Extract(ExtractOptions opt)
    {
      var req = new RepositoryIssueRequest();
      req.State = ItemState.Closed;
      req.SortDirection = SortDirection.Descending;
      req.SortProperty = IssueSort.Created;

      var issues = this.Client.Issue.GetForRepository(opt.Org, opt.Repos, req)
        .Result
        .Select(x => x.ToIssue());

      return new Model.ChangelogData()
        {
          Issues = issues,
          Repository = opt.Repos,
          Owner = opt.Org
        };
    }
  }
}
