namespace Connecto.Repositories
{
    public static class ConnectoFactory
    {
        public static VendorRepository VendorRepository
        {
            get { return new VendorRepository(); }
        }
        public static ReturnReasonRepository ReturnReasonRepository
        {
            get { return new ReturnReasonRepository(); }
        }
        public static SupplierReturnRepository ProductReturnRepository
        {
            get { return new SupplierReturnRepository(); }
        }
        public static ProductRepository ProductRepository
        {
            get { return new ProductRepository(); }
        }
        public static ProductTypeRepository ProductTypeRepository
        {
            get { return new ProductTypeRepository(); }
        }
        public static CompanyRepository CompanyRepository
        {
            get { return new CompanyRepository(); }
        }
        public static LocationRepository LocationRepository
        {
            get { return new LocationRepository(); }
        }
        public static SupplierRepository SupplierRepository
        {
            get { return new SupplierRepository(); }
        }
        public static EmployeeRepository EmployeeRepository
        {
            get { return new EmployeeRepository(); }
        }
        public static CustomerRepository CustomerRepository
        {
            get { return new CustomerRepository(); }
        }
        public static MeasureRepository MeasureRepository
        {
            get { return new MeasureRepository(); }
        }
        public static CurrencyRepository CurrencyRepository
        {
            get { return new CurrencyRepository(); }
        }
        public static ContactRepository ContactRepository
        {
            get { return new ContactRepository(); }
        }
        public static PersonRepository PersonRepository
        {
            get { return new PersonRepository(); }
        }
        public static ProductDetailRepository ProductDetailRepository
        {
            get { return new ProductDetailRepository(); }
        }
        public static SalesDetailRepository SalesDetailRepository
        {
            get { return new SalesDetailRepository(); }
        }
    }
}