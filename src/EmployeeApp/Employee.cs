namespace EmployeeApp;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public Employee(int id, string name, string department, decimal salary)
    {
        this.Id = id;
        this.Name = name;
        this.Department = department;
        this.Salary = salary;
    }
}