namespace Connecto.BusinessObjects
{
    public class Supplier : Connecto
    {
        public int SupplierId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
