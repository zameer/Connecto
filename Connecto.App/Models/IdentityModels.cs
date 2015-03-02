using System.Security.Claims;
using System.Security.Principal;
using Connecto.BusinessObjects;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connecto.App.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int EmployeeId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ConnectoDb")
        {
        }
    }
    public static class GenericPrincipalExtensions
    {
        public static string DisplayName(this IPrincipal user)
        {
            if (!user.Identity.IsAuthenticated) return string.Empty;
            var claimsIdentity = user.Identity as ClaimsIdentity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == "DisplayName")
                    return claim.Value;
            }
            return string.Empty;
        }
        public static int UserId(this IPrincipal user)
        {
            if (!user.Identity.IsAuthenticated) return 0;
            var claimsIdentity = user.Identity as ClaimsIdentity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == "EmployeeId")
                    return int.Parse(claim.Value);
            }
            return 0;
        }
    }
}