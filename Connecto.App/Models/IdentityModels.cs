using System.Security.Claims;
using System.Security.Principal;
using Connecto.BusinessObjects;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace Connecto.App.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int EmployeeId { get; set; }
    }
    public class LocationInfo
    {
        public string DisplayName { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string PrinterName { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("ConnectoDb")
        {
        }
    }
    public static class GenericPrincipalExtensions
    {
        public static LocationInfo LocationInfo(this IPrincipal user)
        {
            if (user == null) return new LocationInfo();
            if (!user.Identity.IsAuthenticated) return new LocationInfo();
            var claimsIdentity = user.Identity as ClaimsIdentity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == "LocationInfo")
                    return JsonConvert.DeserializeObject<LocationInfo>(claim.Value);
            }
            return new LocationInfo();
        }
    }
}