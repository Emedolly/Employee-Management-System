namespace EmployeeApp;

public class EmployeeService
{
    private List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee employee)
    {
        this.employees.Add(employee);
    }

    public List<Employee> GetAll()
    {
        return this.employees;
    }

    public Employee GetById(int id)
    {
        foreach (Employee employee in this.employees)
        {
            if (employee.Id == id)
            {
                return employee;
            }
        }
        return null;
    }

    public bool DeleteById(int id)
    {
        var result = GetById(id);
        if (result== null)
        {
         return false;   
        }
        else
        {
            this.employees.Remove(result);
            return true;
        }
    }

    public bool UpdateById(int id, string name, string department, decimal salary)
    {
        var result = GetById(id);
        if (result == null)
        {
            return false;
        }
        else
        {
            result.Name = name;
            result.Department = department;
            result.Salary = salary;
            return true;
        }
    }

    public decimal CalculateNetSalary(int id)
    {
        Employee emp = GetById(id);
        if (emp == null)
        {
            return 0;
        }
        else
        {
            decimal bonus = emp.Salary * 0.10m;
            decimal tax = emp.Salary * 0.05m;
            return emp.Salary + bonus - tax;
        }
    }

    public void SaveToFile(string path)
    {
        List<string> lines = new List<string>();
        foreach (Employee employee in this.employees)
        {
            lines.Add($"{employee.Id},{employee.Name},{employee.Department},{employee.Salary}");
        }
        File.WriteAllLines(path, lines);
    }
}