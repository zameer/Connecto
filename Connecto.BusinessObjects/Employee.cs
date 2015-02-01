namespace Connecto.BusinessObjects
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
