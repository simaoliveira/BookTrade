namespace BookTrade.Migrations
{
    using BookTrade.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookTrade.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookTrade.Models.ApplicationDbContext context)
        {
            //********Seeds*********
            //utilizador
            var user = new List<Utilizador> {
                new Utilizador {Id=1,DataNasc=new DateTime(1996,10,02),Email="simaoliveira@ipt.pt",NomeCompleto="Simao Pedro De Oliveira Moleiro"},
                new Utilizador {Id=2,DataNasc=new DateTime(1996,05,03),Email="rafandreg@ipt.pt",NomeCompleto="Rafael Andre Campos Goncalves"},
                new Utilizador {Id=3,DataNasc=new DateTime(1995,10,02),Email="tixinha@ipt.pt",NomeCompleto="Patricia Sofia Magalhaes Faustino"},
                new Utilizador {Id=4,DataNasc=new DateTime(1995,07,02),Email="bokica@ipt.pt",NomeCompleto="Beatriz Bangura da Silva Okica de Sa"},
                new Utilizador {Id=5,DataNasc=new DateTime(1999,06,16),Email="cmartins@ipt.pt",NomeCompleto="Claudia Marisa Ferreira Martins"},
                new Utilizador {Id=6,DataNasc=new DateTime(1992,05,18),Email="sdias@ipt.pt",NomeCompleto="Ana Sofia Mendes Dias"},
            };
            user.ForEach(dd => context.Utilizador.AddOrUpdate(d => d.Id, dd));
            context.SaveChanges();


            var storeR = new RoleStore<IdentityRole>(context);
            var managerR = new RoleManager<IdentityRole>(storeR);

            if (!context.Roles.Any(r => r.Name == "Admin")) {
                var role = new IdentityRole { Name = "Admin" };

                managerR.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Viewer")) {
                var role = new IdentityRole { Name = "Viewer" };

                managerR.Create(role);
            }

            /////////////////////////// USERS ///////////////////////////////////
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            /////////////////////////// ADMIN ///////////////////////////////////
            var us = user[0];
            if (!context.Users.Any(u => u.UserName == us.Email)) {
                var u = new ApplicationUser {
                    UserName = us.Email,
                    Email = us.Email
                };

                manager.Create(u, "123qweQWE#");
                manager.AddToRole(u.Id, "Admin");
            }

            for (int i = 1; i < user.Count(); i++) {
                var us2 = user[i];
                if (!context.Users.Any(u => u.UserName == us2.Email)) {
                    var u = new ApplicationUser {
                        UserName = us2.Email,
                        Email = us2.Email
                    };

                    manager.Create(u, "123qweQWE#");
                    manager.AddToRole(u.Id, "Viewer");
                }
            }

            //Autores
            var autor = new List<Autor> {
                new Autor {Id=1,Nome="Paulo Duarte", DataNasc=new DateTime(1979,03,14),Descricao="Paulo Duarte, sj. (1979 - Portimão) - Jesuíta, padre, coordenador da pastoral e professor no Colégio das Caldinhas (Sto. Tirso). São muitas as pessoas que acompanha diariamente: em aulas, em conversas, em celebração de missa e reconciliação. ¿A presença nas redes sociais e ocasionalmente em programas televisivos permite uma comunicação¿ ampla com a sociedade atual.",Fotografia="pauloduarte.jpg"},
                new Autor {Id=2,Nome="Raul Minh'alma", DataNasc=new DateTime(1992,05,14),Descricao="Nasceu em 1992, é natural do Marco de Canaveses, formado em Engenharia Mecânica na Faculdade de Engenharia da Universidade do Porto. Começou a escrever poesia com dezassete anos e em 2011 lança o seu primeiro livro de poemas com o título",Fotografia="raulminhalma.jpg"},
                new Autor {Id=3,Nome="Heather Morris", DataNasc=new DateTime(1953,07,20),Descricao="Heather Morris nasceu na Nova Zelândia e reside atualmente na Austrália. Durante vários anos, enquanto trabalhava num hospital público em Melbourne, estudou e escreveu argumentos para cinema. ",Fotografia="heathermorris.jpg"},
                new Autor {Id=4,Nome="Helena Sacadura Cabral", DataNasc=new DateTime(1950,10,03),Descricao="Além de colunista de diversos jornais e revistas, mantém colaboração regular em televisão. Autora de duas dezenas de livros, concilia ainda a participação activa com a atualização dos seus quatros blogues.",Fotografia="helenasacaduracabral.jpg"},
                new Autor {Id=5,Nome="Lars Kepler", DataNasc=new DateTime(1960,03,20),Descricao="Lars Kepler é o pseudónimo de uma dupla de escritores de sucesso na Suécia: Alexander Ahndoril e Alexandra Coelho Ahndoril. O Hipnotista, primeiro volume da saga, alcançou um enorme sucesso internacional e foi adaptado ao cinema pela mão do realizador Lasse Hallström",Fotografia="larskepler.jpg"},
                new Autor {Id=6,Nome="Herberto Helder", DataNasc=new DateTime(1980,04,30),Descricao="Herberto Helder nasceu em 1930 no Funchal, onde concluiu o 5.º ano. Em 1948 matriculou-se em Direito mas cedo abandonou esse curso para se inscrever em Filologia Românica, que frequentou durante três anos. Teve inúmeros trabalhos e colaborou em vários periódicos como A Briosa",Fotografia="herbertohelder.jpg"},
            };
            autor.ForEach(dd => context.Autor.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            //Categorias
            var categ = new List<Categorias> {
                new Categorias {Id=1,Nome="Infantis e Juvenis"},
                new Categorias {Id=2,Nome="Literatura"},
                new Categorias {Id=3,Nome="Politica"},
                new Categorias {Id=4,Nome="Crime"},
                new Categorias {Id=5,Nome="História"},
                new Categorias {Id=6,Nome="Religião"},
            };
            categ.ForEach(dd => context.Categorias.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            //Livros
            var livro = new List<Livro> {
                new Livro {Id=1,Titulo="O Caçador",Sinopse="A noite tinha acabado de cair, quando Sofia entra numa mansão nos arredores de Estocolmo, onde o seu cliente - um homem muito abastado que nunca viu - a espera. Talvez seja por isso que Sofia avança furtivamente, como um animal selvagem.",AnoLanc=2018,Editora="Porto Editora",Idioma="Português",NumeroDePaginas=568,AutorId=5,Fotografia="ocacador.jpg"},
                new Livro {Id=2,Titulo="Uma Certa Forma de Vida",Sinopse="Neste livro, a autora começa por abordar temas que marcam a vida moderna, mas que a podem transformar negativamente, se não estivermos atentos. ",AnoLanc=2018,Editora="Clube do Autor",Idioma="Portugues",NumeroDePaginas=184,AutorId=4,Fotografia="umacertaformadevida.jpg"},
                new Livro {Id=3,Titulo="O Tatuador de Auschwitz",Sinopse="Esta é a história assombrosa do tatuador de Auschwitz e da mulher que conquistou o seu coração - um dosepisódios mais extraordinários e inesquecíveis do Holocausto.",AnoLanc=2018,Editora="Editorial Presença",Idioma="Portugues",NumeroDePaginas=232,AutorId=3,Fotografia="tatuador.jpg"},
                new Livro {Id=4,Titulo="Em minúsculas",Sinopse="Embora não esteja aqui coligida a totalidade das suas colaborações, todos os textos deste livro foram publicados no Notícia - Semanário Ilustrado, no período em que Herberto Helder viveu em Luanda. ",AnoLanc=2018,Editora="Porto Editora",Idioma="Portugues",NumeroDePaginas=200,AutorId=6,Fotografia="emminusculas.jpg"},
                new Livro {Id=5,Titulo="Dá-me um Dia para Mudar a Tua Vida",Sinopse=" Aqui encontras 500 mensagens inspiradoras para leres e refletires no teu dia a dia. Serão a tua bússola.",AnoLanc=2018,Editora="Manuscrito Editora",Idioma="Portugues",NumeroDePaginas=200,AutorId=2,Fotografia="dameumdia.jpg"},
                new Livro {Id=6,Titulo="Deus Como Tu",Sinopse="Deus Como Tu conduz-nos num regresso à fé, humana, vivida no quotidiano, e coloca-nos questões que nos fazem a pensar em temas como a morte, a culpa, a solidão, mas também, o riso, a alegria e a liberdade.",AnoLanc=2018,Editora="Matéria Prima",Idioma="Portugues",NumeroDePaginas=168,AutorId=1,Fotografia="deuscomotu.jpg"},
            };
            livro.ForEach(dd => context.Livro.AddOrUpdate(d => d.Titulo, dd));
            context.SaveChanges();

            //Comentários
            var coment = new List<Comentarios> {
                new Comentarios {Id=1,Texto="Adoro! Muito Emocionante",Data=new DateTime(2016,10,03),LivroId=3,UtilizadorId=1},
                new Comentarios {Id=2,Texto="Muito Bom, recomendo!",Data=new DateTime(2017,03,10),LivroId=4,UtilizadorId=5},
                new Comentarios {Id=3,Texto="5 estrelas, Fantastico",Data=new DateTime(2018,01,26),LivroId=1,UtilizadorId=3},
                new Comentarios {Id=4,Texto="Ficou um pouco áquem das expectativas",Data=new DateTime(2017,12,06),LivroId=2,UtilizadorId=6},
                new Comentarios {Id=5,Texto="bom mas tedioso",Data=new DateTime(2016,05,15),LivroId=6,UtilizadorId=4},
                new Comentarios {Id=6,Texto="Teve um bom inicio mas um péssimo final",Data=new DateTime(2017,11,17),LivroId=5,UtilizadorId=2},
            };
            coment.ForEach(dd => context.Comentarios.AddOrUpdate(d => d.Texto, dd));
            context.SaveChanges();
        }
    }
}
