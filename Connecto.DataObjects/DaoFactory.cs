namespace Connecto.DataObjects
{
    /// <summary>
    /// Abstract factory interface, creates data access object
    /// </summary>
    /// <remarks>
    /// Factory design pattern
    /// </remarks>
    public interface IDaoFactory
    {
        IVendorDao VendorDao { get; }
        //ISupplierReturnDao SupplierReturnDao { get; }
        IReturnReasonDao ReturnReasonDao { get; }
        IProductDao ProductDao { get; }
        IProductTypeDao ProductTypeDao { get; }
        ICompanyDao CompanyDao { get; }
        ISupplierDao SupplierDao { get; }
        IEmployeeDao EmployeeDao { get; }
        ICustomerDao CustomerDao { get; }
        IMeasureDao MeasureDao { get; }
        ICurrencyDao CurrencyDao { get; }
        IContactDao ContactDao { get; }
        IPersonDao PersonDao { get; }
        IProductDetailDao ProductDetailDao { get; }
        ISalesDetailDao SalesDetailDao { get; }
        ICustomerReturnDao CustomerReturnDao { get; }
    }
}
