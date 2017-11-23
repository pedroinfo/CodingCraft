namespace CodingCraftHOMod1Ex1EF.Migrations
{
    using CodingCraftHOMod1Ex1EF.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraftHOMod1Ex1EF.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CodingCraftHOMod1Ex1EF.Models.ApplicationDbContext context)
        {
            context.Categorias.AddOrUpdate(
              c => c.Nome,
              new Categoria { Nome = "Chocolates" },
              new Categoria { Nome = "Salgados" },
              new Categoria { Nome = "Refrigerantes" }
            );

            context.SaveChanges();

            context.Produtos.AddOrUpdate(
                p => p.Nome,
                new Produto { Nome = "Chocolate Alpino", CategoriaId = 1, Preco = 1 },
                new Produto { Nome = "Amendoim Mendorato", CategoriaId = 2, Preco = 1 },
                new Produto { Nome = "Coca Cola", CategoriaId = 3, Preco = 1 }
            );

            context.SaveChanges();
        }
    }
}
