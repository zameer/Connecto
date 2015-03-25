
namespace Connecto.BusinessObjects
{
    public class Customer : Connecto
    {
        public int CustomerId { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
