namespace EmployeeApp;

public class Employee
{
    public int Id { get; }
    public string Name { get; set; }
    public string Department { get; set; }
    private decimal _salary;

    public decimal Salary
    {
        get { return _salary;}
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException("Salary", "Salary cannot be less than 0");
            _salary = value;
        }
    }

    public Employee(int id, string name, string department, decimal salary)
    {
        this.Id = id;
        this.Name = name;
        this.Department = department;
        Salary = salary;
    }
}