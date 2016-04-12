using System.Data.Entity;

namespace RepoLocalDB
{
    public class NodePadContext : DbContext
    {
        public NodePadContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NodePadContext, Migrations.Configuration>());

        }

        public DbSet<VectorRecord> VectorRecords { get; set; }

        public DbSet<MatrixRecord> MatrixRecords { get; set; }

    }
}
