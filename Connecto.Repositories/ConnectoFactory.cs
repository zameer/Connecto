namespace Connecto.Repositories
{
    public static class ConnectoFactory
    {
        public static VendorRepository VendorRepository
        {
            get { return new VendorRepository(); }
        }
        public static ProductRepository ProductRepository
        {
            get { return new ProductRepository(); }
        }
        public static CompanyRepository CompanyRepository
        {
            get { return new CompanyRepository(); }
        }
        public static LocationRepository LocationRepository
        {
            get { return new LocationRepository(); }
        }
    }
}