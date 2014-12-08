namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework specific factory that creates data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory.
    /// </remarks>
    public class EntityDaoFactory : IDaoFactory
    {
        /// <summary>
        /// Gets an Entity Framework specific vendor data access object.
        /// </summary>
        public IVendorDao VendorDao {
            get { return new EntityVendorDao(); }
        }
        public IProductDao ProductDao
        {
            get { return new EntityProductDao(); }
        }
        public ICompanyDao CompanyDao
        {
            get { return new EntityCompanyDao(); }
        }
    }
}
