﻿using System;
using FluentNHibernate.Cfg.Db;
using Suteki.Shop.Database;
using Suteki.Shop.Repositories;

namespace Suteki.Shop.CreateDb
{
    /// <summary>
    /// Run this at the command line:
    /// Suteki.Shop.CreateDb
    /// To create the Suteki Shop Database.
    /// </summary>
    class Program
    {
        static void Main()
        {
            var configurationBuilder = new FluentNHibernateConfigurationBuilder();
            var configuration = configurationBuilder.BuildConfiguration(
                MsSqlConfiguration.MsSql2005.ConnectionString(
                    c => c.FromConnectionStringWithKey("SutekiShopConnectionString")));

            var databaseManager = new DatabaseManager(configuration);

            if (databaseManager.DatabaseExists)
            {
                Console.WriteLine("Dropping Database {0}", databaseManager.GetDbName());
                databaseManager.DropDatabase();
            }
            Console.WriteLine("Creating Database {0}", databaseManager.GetDbName());
            databaseManager.CreateDatabase();

            Console.WriteLine("Inserting Static Data");
            new StaticDataGenerator(configuration).Insert();

            Console.WriteLine("Successfully Created Database {0}", databaseManager.GetDbName());
        }
    }
}
