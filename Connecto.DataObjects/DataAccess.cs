using Connecto.DataObjects.EntityFramework.Implementation;

namespace Connecto.DataObjects
{
    public static class DataAccess
    {
        // The static field initializers below are thread safe.
        private static readonly IDaoFactory _factory = new EntityDaoFactory();

        /// <summary>
        /// Gets a provider-specific vendor data access object.
        /// </summary>
        public static IVendorDao VendorDao
        {
            get { return _factory.VendorDao; }
        }

        public static IProductDao ProductDao
        {
            get { return _factory.ProductDao; }
        }

        public static IProductTypeDao ProductTypeDao
        {
            get { return _factory.ProductTypeDao; }
        }

        public static ICompanyDao CompanyDao
        {
            get { return _factory.CompanyDao; }
        }

        public static ISupplierDao SupplierDao
        {
            get { return _factory.SupplierDao; }
        }

        public static IEmployeeDao EmployeeDao
        {
            get { return _factory.EmployeeDao; }
        }

        public static ICustomerDao CustomerDao
        {
            get { return _factory.CustomerDao; }
        }

        public static IMeasureDao MeasureDao
        {
            get { return _factory.MeasureDao; }
        }

        public static ICurrencyDao CurrencyDao
        {
            get { return _factory.CurrencyDao; }
        }

        public static IContactDao ContactDao
        {
            get { return _factory.ContactDao; }
        }

        public static IPersonDao PersonDao
        {
            get { return _factory.PersonDao; }
        }

        public static IProductDetailDao ProductDetailDao
        {
            get { return _factory.ProductDetailDao; }
        }

        public static ISalesDetailDao SalesDetailDao
        {
            get { return _factory.SalesDetailDao; }
        }
    }
}
