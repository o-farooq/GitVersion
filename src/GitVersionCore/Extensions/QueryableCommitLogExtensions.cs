namespace GitVersion.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using LibGit2Sharp;

    public static class QueryableCommitLogExtensions
    {
        public static IEnumerable<Commit> QueryByPath(this IQueryableCommitLog commitLog, string path, CommitFilter commitFilter)
        {
            if (string.IsNullOrEmpty(path))
            {
                return commitLog.QueryBy(commitFilter);
            }

            return commitLog.QueryBy(path, commitFilter).Select(c => c.Commit);
        }

        public static IEnumerable<Commit> QueryByPath(this IQueryableCommitLog commitLog, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return commitLog;
            }

            return commitLog.QueryBy(path, new CommitFilter()).Select(c => c.Commit);
        }
    }
}