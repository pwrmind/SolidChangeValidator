namespace Ideas;

using System.Collections.Generic;

using LibGit2Sharp;

public class CodeChange
{
    public string ChangeId { get; }
    public List<SolidPrinciple> ViolatedPrinciples { get; } = new List<SolidPrinciple>();
    public string Description { get; }
    public string Diff { get; }

    public CodeChange(string changeId, string description, string gitRepositoryPath, string filePath)
    {
        ChangeId = changeId;
        Description = description;
        Diff = GetDiffFromGit(gitRepositoryPath, filePath);
    }

    private string GetDiffFromGit(string gitRepositoryPath, string filePath)
    {
        using (var repo = new Repository(gitRepositoryPath))
        {
            var patch = repo.Diff.Compare<Patch>(repo.Head.Tip.Tree, repo.Head.Tip.Tree);
            return patch.Content;
        }
    }
}
