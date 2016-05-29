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
        public IReturnReasonDao ReturnReasonDao
        {
            get { return new EntityReturnReasonDao(); }
        }
        public ISupplierDao SupplierDao
        {
            get { return new EntitySupplierDao(); }
        }

        public IProductDao ProductDao
        {
            get { return new EntityProductDao(); }
        }

        public IProductTypeDao ProductTypeDao
        {
            get { return new EntityProductTypeDao(); }
        }

        public ICompanyDao CompanyDao
        {
            get { return new EntityCompanyDao(); }
        }

        public IEmployeeDao EmployeeDao
        {
            get { return new EntityEmployeeDao(); }
        }

        public ICustomerDao CustomerDao
        {
            get { return new EntityCustomerDao(); }
        }

        public IMeasureDao MeasureDao
        {
            get { return new EntityMeasureDao(); }
        }

        public ICurrencyDao CurrencyDao
        {
            get { return new EntityCurrencyDao(); }
        }

        public IContactDao ContactDao
        {
            get { return new EntityContactDao(); }
        }
        public IPersonDao PersonDao
        {
            get { return new EntityPersonDao(); }
        }

        public IProductDetailDao ProductDetailDao
        {
            get { return new EntityProductDetailDao(); }
        }

        public ISalesDetailDao SalesDetailDao
        {
            get { return new EntitySalesDetailDao(); }
        }

        public IWriteoffDetailDao WriteoffDetailDao
        {
            get { return new EntityWriteoffDetailDao(); }
        }

    }
}
