namespace CodingCraftHOMod1Ex7Redis.Migrations
{
    using CodingCraftHOMod1Ex7Redis.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraftHOMod1Ex7Redis.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            for (int i = 0; i < 100; i++)
            {
                string valor = i.ToString().PadLeft(2, '0');

                context.Compromissos.AddOrUpdate(new Compromisso()
                {
                    DataHora = DateTime.Now,
                    Local = "Local " + valor,
                    Titulo = "Título " + valor,
                    Observacoes = "Obs. " + valor
                });
                
                context.Empresas.AddOrUpdate(new Empresa()
                {
                    Cnpj =  valor +  ".000.000/0000-00", //75.737.167/0001-39
                    NomeFantasia = "Empresa - " + valor,
                    RazaoSocial = "Razão - " + valor
                });

                context.Contatos.AddOrUpdate(new Contato()
                {
                    Nome = "Nome " + valor,
                    Email = "email" + valor + "@hotmail.com",
                    Telefone = "(13)9987 10" + valor,
                    Observacao = "Observação " + valor
                });

                context.Veiculos.AddOrUpdate(new Veiculo()
                {
                    Placa = "AAA-10" + valor,
                    Ano = 1900,
                    Carro = "Carro " + valor,
                    Marca = "Marca " + valor
                });
                
                context.Funcionarios.AddOrUpdate(new Funcionario()
                {
                    Nome = "João " + valor,
                    Cargo = "Cargo " + valor,
                    DataNascimento = DateTime.Now
                });
                
                                context.Bancos.AddOrUpdate(new Banco()
                                {
                                    Nome = "Banco",
                                    Numero = i
                                });
                
                                if (i <= 9)
                                {
                                    context.Categorias.AddOrUpdate(new Categoria()
                                    {
                                        Nome = "Categoria " + valor

                                    });
                                }
                
                context.Produtos.AddOrUpdate(new Produto()
                {
                    Nome = "Produto " + valor,
                    Preco = Convert.ToDecimal(valor) + 57,
                    CategoriaId = context.Categorias.FirstOrDefault().CategoriaId
                });
            }



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
