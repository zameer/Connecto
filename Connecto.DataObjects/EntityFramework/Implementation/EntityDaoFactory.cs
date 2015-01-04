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

        /// <summary>
        /// Gets an Entity Framework specific supplier data access object.
        /// </summary>
        public ISupplierDao SupplierDao
        {
            get { return new EntitySupplierDao(); }
        }

        /// <summary>
        /// Gets an Entity Framework specific product data access object.
        /// </summary>
        public IProductDao ProductDao
        {
            get { return new EntityProductDao(); }
        }

        /// <summary>
        /// Gets an Entity Framework specific company data access object.
        /// </summary>
        public ICompanyDao CompanyDao
        {
            get { return new EntityCompanyDao(); }
        }

        /// <summary>
        /// Gets an Entity Framework specific employee data access object.
        /// </summary>
        public IEmployeeDao EmployeeDao
        {
            get { return new EntityEmployeeDao(); }
        }

        /// <summary>
        /// Gets an Entity Framework specific measure data access object.
        /// </summary>
        public IMeasureDao MeasureDao
        {
            get { return new EntityMeasureDao(); }
        }

        /// <summary>
        /// Gets an Entity Framework specific currency data access object.
        /// </summary>
        public ICurrencyDao CurrencyDao
        {
            get { return new EntityCurrencyDao(); }
        }

        /// <summary>
        /// Gets an Entity Framework specific Contact data access object.
        /// </summary>
        public IContactDao ContactDao
        {
            get { return new EntityContactDao(); }
        }
    }
}
