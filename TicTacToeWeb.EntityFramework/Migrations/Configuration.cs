using System.Data.Entity.Migrations;

namespace TicTacToeWeb.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TicTacToeWeb.EntityFramework.TicTacToeWebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TicTacToeWeb";
        }

        protected override void Seed(TicTacToeWeb.EntityFramework.TicTacToeWebDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
