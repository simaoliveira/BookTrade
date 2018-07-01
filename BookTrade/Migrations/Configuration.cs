namespace BookTrade.Migrations {
    using BookTrade.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookTrade.Models.ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(BookTrade.Models.ApplicationDbContext context) {
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

            //Criação das Roles

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
            var us = user[0]; //Primeiro utilizador do seed da tabela de Users
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
                new Autor {Id=1,Nome="Paulo Duarte", DataNasc=new DateTime(1979,03,14),Descricao="Paulo Duarte, sj. (1979 - Portimão) - Jesuíta, padre, coordenador da pastoral e professor no Colégio das Caldinhas (Sto. Tirso). São muitas as pessoas que acompanha diariamente: em aulas, em conversas, em celebração de missa e reconciliação. ¿A presença nas redes sociais e ocasionalmente em programas televisivos permite uma comunicação¿ ampla com a sociedade atual.",AutorFotografia="pauloduarte.jpg"},
                new Autor {Id=2,Nome="Raul Minh'alma", DataNasc=new DateTime(1992,05,14),Descricao="Nasceu em 1992, é natural do Marco de Canaveses, formado em Engenharia Mecânica na Faculdade de Engenharia da Universidade do Porto. Começou a escrever poesia com dezassete anos e em 2011 lança o seu primeiro livro de poemas com o título",AutorFotografia="raulminhalma.jpg"},
                new Autor {Id=3,Nome="Heather Morris", DataNasc=new DateTime(1953,07,20),Descricao="Heather Morris nasceu na Nova Zelândia e reside atualmente na Austrália. Durante vários anos, enquanto trabalhava num hospital público em Melbourne, estudou e escreveu argumentos para cinema. ",AutorFotografia="heathermorris.jpg"},
                new Autor {Id=4,Nome="Helena Sacadura Cabral", DataNasc=new DateTime(1950,10,03),Descricao="Além de colunista de diversos jornais e revistas, mantém colaboração regular em televisão. Autora de duas dezenas de livros, concilia ainda a participação activa com a atualização dos seus quatros blogues.",AutorFotografia="helenasacaduracabral.jpg"},
                new Autor {Id=5,Nome="Lars Kepler", DataNasc=new DateTime(1960,03,20),Descricao="Lars Kepler é o pseudónimo de uma dupla de escritores de sucesso na Suécia: Alexander Ahndoril e Alexandra Coelho Ahndoril. O Hipnotista, primeiro volume da saga, alcançou um enorme sucesso internacional e foi adaptado ao cinema pela mão do realizador Lasse Hallström",AutorFotografia="larskepler.jpg"},
                new Autor {Id=6,Nome="Herberto Helder", DataNasc=new DateTime(1980,04,30),Descricao="Herberto Helder nasceu em 1930 no Funchal, onde concluiu o 5.º ano. Em 1948 matriculou-se em Direito mas cedo abandonou esse curso para se inscrever em Filologia Românica, que frequentou durante três anos. Teve inúmeros trabalhos e colaborou em vários periódicos como A Briosa",AutorFotografia="herbertohelder.jpg"},
                new Autor {Id=7,Nome="Maria Teresa Gonzales", DataNasc=new DateTime(1958,06,17),Descricao="Maria Teresa Maia Gonzalez, licenciada em Línguas e Literaturas Modernas, co-autora da colecção 'O Clube das Chaves', é autora de inúmeras outras obras, incluindo vários títulos premiados. 'A Lua de Joana', o seu maior sucesso editorial. O seu livro, 'O Pai no Tecto', foi igualmente bem recebido pelos jovens leitores e professores. É uma das mais vendidas e prestigiadas autoras portuguesas de livros dedicados a crianças e jovens adolescentes. Inicia com a publicação deste livro na Principia, uma colaboração na área dos livros para crianças e jovens adolescentes, com livros cujo conteúdo pretende transmitir uma mensagem espiritual e um sentido de religiosidade adaptado à idade do público alvo.",AutorFotografia="MariaTeresaGoncalves.jpg"},
                new Autor {Id=8,Nome="Mark Manson", DataNasc=new DateTime(1984,03,09),Descricao="Mark Manson cresceu em Austin, no Texas, viveu em Boston e viajou por todo o mundo durante sete anos. É um autor bestseller do The New York Times e escreve sobre uma grande variedade de temas, no âmbito do desenvolvimento pessoal. Para além da sua atividade de bloguer e empreendedor, publica regularmente artigos com a BBC, CNN, Business Insider, Time, entre outros. Vive atualmente em Nova Iorque.",AutorFotografia="markmanson.jpg"},
                new Autor {Id=9,Nome="Afonso Noite-Luar", DataNasc=new DateTime(1992,06,17),Descricao="Eu sou o Afonso Noite-Luar. Basta-te saber isso.Afonso só há um, não invoques o meu nome em vão.",AutorFotografia="afonsonoiteluar.jpg"},
                new Autor {Id=10,Nome="Joël Dicker", DataNasc=new DateTime(1985,06,16),Descricao="Joël Dicker nasceu em Genève, Suíça, em 1985. A verdade sobre o caso Harry Quebert é o seu segundo romance, com o qual arrecadou vários prémios: Prix de la Vocation Bleustein-Blanchet, o Grande Prémio do Romance da Academia Francesa, o Prémio Goncourt des Lycéens e o prémio da revista Lire para Melhor Romance em língua francesa. O seu primeiro romance, Les derniers jours de nos pères, venceu o Prémio dos Escritores de Genève.",AutorFotografia="joeldicker.jpg"},
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
                new Categorias {Id=7,Nome="Romance"},
                new Categorias {Id=8,Nome="Aventura"},
                new Categorias {Id=9,Nome="Auto Ajuda"},
                new Categorias {Id=10,Nome="Memórias e Testemunhos"},
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
                new Livro {Id=7,Titulo="A Lua de Joana",Sinopse="Ao lermos a Lua de Joana, não podemos deixar de pensar na forma como, muitas vezes, relegamos para segundo plano aquilo que realmente é importante na vida. Porque este livro alerta-nos para a importância de estarmos atentos a nós e ao outro, e de sermos capazes de, em conjunto, percorrer um caminho que conduza a uma vida plena…",AnoLanc=2010,Editora="PI",Idioma="Portugues",NumeroDePaginas=184,AutorId=7,Fotografia="aluadejoana.jpg"},
                new Livro {Id=8,Titulo="A Arte Subtil de Saber Dizer Que Se F*da",Sinopse="Durante décadas convenceram-nos de que o pensamento positivo era a chave para uma vida rica e feliz. Mas esses dias chegaram ao fim. Que se f*da o pensamento positivo! Mark Manson acredita que a sociedade está contaminada por grandes doses de treta e de expectativas ilusórias em relação a nós próprios e ao mundo.",AnoLanc=2018,Editora="Desassossego",Idioma="Portugues",NumeroDePaginas=208,AutorId=8,Fotografia="aartesubtil.jpg"},
                new Livro {Id=9,Titulo="Ela Primeiro",Sinopse="«Vais apaixonar-te por mim dentro de um mês». Inês não precisou de tanto tempo para se perder no labirinto sedutor de Afonso e, aquilo que começou por ser um encontro casual no parque, rapidamente se tornou num complexo jogo de sedução e sexo.",AnoLanc=2018,Editora="Manuscrito Editora",Idioma="Portugues",NumeroDePaginas=312,AutorId=9,Fotografia="elaprimeiro.jpg"},
                new Livro {Id=10,Titulo="O Desaparecimento de Stephanie Mailer",Sinopse="Na noite de 30 de Julho de 1994, a pacata vila de Orphea, na costa leste dos Estados Unidos, assiste ao grande espectáculo de abertura do festival de teatro. Mas o presidente da Câmara está atrasado para a cerimónia… Ao mesmo tempo, Samuel Paladin percorre as ruas desertas da vila à procura da mulher, que saiu para correr e não voltou. Só para quando encontra o seu corpo em frente à casa do presidente da Câmara. Dentro da casa, toda a família do presidente está morta.",AnoLanc=2018,Editora="Alfaguara Portugal",Idioma="Portugues",NumeroDePaginas=640,AutorId=10,Fotografia="odesaparecimento.jpg"},
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
                new Comentarios {Id=7,Texto="Demasiado Tedioso",Data=new DateTime(2016,11,17),LivroId=5,UtilizadorId=3},
                new Comentarios {Id=8,Texto="Assunto Pouco Interessante",Data=new DateTime(2017,03,20),LivroId=3,UtilizadorId=4},
                new Comentarios {Id=9,Texto="Ótimo Livro, torna-nos muito pensativos",Data=new DateTime(2015,09,01),LivroId=7,UtilizadorId=6},
                new Comentarios {Id=10,Texto="Final fantastico!",Data=new DateTime(2016,07,27),LivroId=5,UtilizadorId=1},
            };
            coment.ForEach(dd => context.Comentarios.AddOrUpdate(d => d.Texto, dd));
            context.SaveChanges();
        }
    }
}
