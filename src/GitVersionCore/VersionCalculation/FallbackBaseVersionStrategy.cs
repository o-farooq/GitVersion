namespace GitVersion.VersionCalculation
{
    using System.Collections.Generic;
    using System.Linq;
    using BaseVersionCalculators;
    using GitVersion.Extensions;
    using LibGit2Sharp;

    /// <summary>
    /// Version is 0.1.0.
    /// BaseVersionSource is the "root" commit reachable from the current commit.
    /// Does not increment.
    /// </summary>
    public class FallbackBaseVersionStrategy : BaseVersionStrategy
    {
        public override IEnumerable<BaseVersion> GetVersions(GitVersionContext context)
        {
            Commit baseVersionSource;
            var currentBranchTip = context.CurrentBranch.GetTip(context.PathFilter);

            try
            {
                baseVersionSource = context.Repository.Commits.QueryByPath(context.PathFilter, new CommitFilter
                {
                    IncludeReachableFrom = currentBranchTip
                }).Last();
            }
            catch (NotFoundException exception)
            {
                throw new GitVersionException($"Can't find commit {currentBranchTip.Sha}. Please ensure that the repository is an unshallow clone with `git fetch --unshallow`.", exception);
            }

            yield return new BaseVersion(context, "Fallback base version", false, new SemanticVersion(minor : 1), baseVersionSource, null);
        }
    }
}