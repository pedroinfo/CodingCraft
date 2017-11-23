namespace EX11.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Categoria.AddOrUpdate(
                c => c.Nome,
                new Categoria { Nome = "Categoria 1" },
                new Categoria { Nome = "Categoria 2" },
                new Categoria { Nome = "Categoria 3" }
                );

            context.SaveChanges();

            context.Indicador.AddOrUpdate(
                i => i.Nome,
                new Indicador { Nome = "Indicador 1" },
                new Indicador { Nome = "Indicador 2" },
                new Indicador { Nome = "Indicador 3" }
                );

            context.SaveChanges();

            context.Pais.AddOrUpdate(
                p => p.Nome,
                new Pais { Nome = "Brasil" },
                new Pais { Nome = "Paraguai" },
                new Pais { Nome = "Uruguai" },
                new Pais { Nome = "Venezuela" }
                );

            context.SaveChanges();

            context.Pesquisas.AddOrUpdate(
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 1, Ano = 2010 },
                    new Pesquisa { PaisId = 2, CategoriaId = 2, IndicadorId = 1, Ano = 2012 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2017 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2013 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2015 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2012 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2000 },
                    new Pesquisa { PaisId = 1, CategoriaId = 1, IndicadorId = 2, Ano = 2011 }
                );

            context.SaveChanges();

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
