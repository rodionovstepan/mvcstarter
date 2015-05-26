namespace MvcStarter.Migrator
{
    using System;
    using System.Linq;
    using System.Reflection;
    using PostgreSql;
    using PostgreSql.Extensions;
    using PostgreSql.Impl;

    public static class MigrationRunner
    {
        private static readonly IConnectionFactory ConnectionFactory = new PostgresqlConnectionFactory();

        public static void Run()
        {
            var defaultColor = Console.ForegroundColor;
            var migrations = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Migration)))
                .Select(Activator.CreateInstance)
                .OfType<Migration>()
                .OrderBy(x => x.Id)
                .ToList();

            if (migrations.Count == 0)
            {
                Console.WriteLine("No migrations found.");
                return;
            }

            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(
                            new QueryObject(
                                "CREATE TABLE IF NOT EXISTS migrations (id bigint primary key, migrated timestamp)"),
                            transaction
                            );

                        foreach (var migration in migrations)
                        {
                            Console.WriteLine("---");
                            Console.WriteLine("Migrating " + migration.Id + "...");

                            var migrationId = connection.Single<long>(
                                new QueryObject("SELECT id FROM migrations WHERE id = @Id", new { migration.Id })
                                );

                            if (migrationId == 0)
                            {
                                migration.Run(connection, transaction);
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Migration " + migration.Id + " is now executed");
                                Console.ForegroundColor = defaultColor;
                            }
                            else
                            {
                                Console.WriteLine("Migration " + migration.Id + " were already executed");
                            }

                            Console.WriteLine();
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine("---");
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("---");
                        Console.WriteLine("Transaction failed! Rollback.");
                        Console.WriteLine("---");
                        Console.ForegroundColor = defaultColor;

                        transaction.Rollback();
                    }
                }
            }

            Console.WriteLine();
        }
    }
}