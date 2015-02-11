using Connecto.App.Models;
using Connecto.BusinessObjects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connecto.App.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Connecto.App.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Connecto.App.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            if (!context.Users.Any(u => u.UserName.Equals("connecto")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "connecto", DisplayName = "Connecto"};
                manager.Create(user, "developer");
            }
        }
    }
}
