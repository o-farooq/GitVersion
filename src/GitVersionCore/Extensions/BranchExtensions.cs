namespace GitVersion.Extensions
{
    using System.Linq;
    using LibGit2Sharp;

    public static class BranchExtensions
    {
        public static Commit GetTip(this Branch branch, string path)
        {
            return string.IsNullOrEmpty(path) ? branch.Tip : (branch.Commits as CommitLog).QueryBy(path).First().Commit;
        }
    }
}