using BookTrade.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookTrade.Startup))]
namespace BookTrade
{
    public partial class Startup
    {
        //Este método é executado apenas uma vez quando a aplicação arranca
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //invoca método para inicio das configurações dos Roles
            initAplica();
        }

        //Cria, em caso de falta, as roles de suporte, Admin e Utilizador
        //Cria, no mesmo caso, um utilizador

        private void initAplica() {

                ApplicationDbContext db = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                // criar a Role 'Admin'
                if (!roleManager.RoleExists("Admin")) {
                    // não existe a 'role'
                    // então, criar essa role
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                }

                // criar a Role 'Condutores'
                if (!roleManager.RoleExists("Utilizador")) {
                    // não existe a 'role'
                    // então, criar essa role
                    var role = new IdentityRole();
                    role.Name = "Utilizador";
                    roleManager.Create(role);
                }

                // criar um utilizador 'Admin'
                var user = new ApplicationUser();
                user.UserName = "simaoliveira@ipt.pt";
                user.Email = "simaoliveira@ipt.pt";
                string userPWD = "123qweQWE#";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Admin-
                if (chkUser.Succeeded) {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }
        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97

    }
}
