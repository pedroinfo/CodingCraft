namespace CodingCraftHOMod1Ex5WebAPI.Migrations
{
    using Models;
    using Models.Contexto;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraftHOMod1Ex5WebAPI.Models.Contexto.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.ArquivosTipos.AddOrUpdate(
                p => p.Descricao,
                new ArquivoTipo() { Descricao = "audio" },
                new ArquivoTipo() { Descricao = "image" },
                new ArquivoTipo() { Descricao = "outros" },
                new ArquivoTipo() { Descricao = "ppt" },
                new ArquivoTipo() { Descricao = "text" },
                new ArquivoTipo() { Descricao = "video" },
                new ArquivoTipo() { Descricao = "word" },
                new ArquivoTipo() { Descricao = "excel" }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
