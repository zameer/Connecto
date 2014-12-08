using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.DataObjects.EntityFramework
{
    /// <summary>
    /// POCO: BizLiteManagerEntities
    /// </summary>
    /// <remarks>
    /// When running DataObjects library to generate or update database
    /// this will use App.config connectionStrings 'BizLiteManagerEntities'
    /// </remarks>
    public class ConnectoManagerEntities : DbContext
    {
        /// <summary>
        /// This constructor will help to init with default connectionstring
        /// </summary>
        public ConnectoManagerEntities() : base("name=ConnectoDb") { }

        public DbSet<EntityVendor> Vendors { get; set; }
        public DbSet<EntityProduct> Products { get; set; }
        public DbSet<EntityCompany> Companies { get; set; }
        public DbSet<EntityCompanyLocation> CompanyLocations { get; set; }
    }
}
