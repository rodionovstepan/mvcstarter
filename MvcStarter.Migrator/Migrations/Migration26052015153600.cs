namespace MvcStarter.Migrator.Migrations
{
    public class Migration26052015153600 : Migration
    {
        public override long Id
        {
            get { return 26052015153600; }
        }

        public override void MigrationScenario()
        {
            Execute("UPDATE users SET username = 'fixed' WHERE id = 1");
        }
    }
}