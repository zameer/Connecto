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
        public DbSet<EntitySupplier> Suppliers { get; set; }
        public DbSet<EntityProduct> Products { get; set; }
        public DbSet<EntityCompany> Companies { get; set; }
        public DbSet<EntityCompanyLocation> CompanyLocations { get; set; }
        public DbSet<EntityEmployee> Employees { get; set; }
        public DbSet<EntityCurrency> Currencys { get; set; }
        public DbSet<EntityProductDetail> ProductDetails { get; set; }
        public DbSet<EntityProductDetailCart> ProductDetailCarts { get; set; }
        public DbSet<EntityProductSupplier> ProductSuppliers { get; set; }
        public DbSet<EntityMeasure> Measures { get; set; }
        public DbSet<EntityContact> Contacts { get; set; }
        public DbSet<EntityPerson> People { get; set; }
        public DbSet<EntityProductType> ProductTypes { get; set; }
        public DbSet<EntityOrder> Orders { get; set; }
        public DbSet<EntityCustomer> Customers { get; set; }
        public DbSet<EntitySalesDetail> SalesDetails { get; set; }
        public DbSet<EntitySalesDetailCart> SalesDetailCarts { get; set; }
    }
}
