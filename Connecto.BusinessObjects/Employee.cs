namespace Connecto.BusinessObjects
{
    public class Employee : Connecto
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
