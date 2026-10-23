namespace EmployeeApp;

public class EmployeeService
{
    private readonly List<Employee> _employees = new List<Employee>();
    public event Action<Employee> EmployeeAdded; //the doorbell

    public void AddEmployee(Employee employee)
    {
        if (GetById(employee.Id) != null)
            throw new ArgumentException("Id already exists");
        _employees.Add(employee);
        EmployeeAdded?.Invoke(employee); //ring the bell at last
    }

    public List<Employee> GetAll()
    {
        return _employees;
    }

    public Employee GetById(int id)
    {
        return _employees.FirstOrDefault(e=>e.Id==id);
        // foreach (Employee employee in _employees)
        // {
        //     if (employee.Id == id)
        //     {
        //         return employee;
        //     }
        // }
        // return null;
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
            _employees.Remove(result);
            return true;
        }
    }

    public bool UpdateById(int id, string name, string department, decimal salary)
    {
        Employee employee = GetById(id);
        if (employee == null)
            return false;

        employee.Salary = salary;                                 // FIRST: a bad salary stops everything
        employee.Name = name;
        employee.Department = department;
        return true;
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
        foreach (Employee employee in _employees)
        {
            //lines.Add($"{employee.Id},{employee.Name},{employee.Department},{employee.Salary}");
            lines.Add(employee.ToCsvLine());
        }
        File.WriteAllLines(path, lines);
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }
        else
        {
            string[] lines = File.ReadAllLines(path);
            _employees.Clear();
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length != 4)
                {
                    continue;
                }

                if (!int.TryParse(parts[0], out int id))
                {
                    continue;
                }

                if (!decimal.TryParse(parts[3], out decimal salary))
                {
                    continue;
                }
                _employees.Add(new Employee(id, parts[1], parts[2], salary));
            }
          
        }
    }

    public List<Employee> Filter(Func<Employee, bool> rule)
    {
        // List<Employee> result = new List<Employee>();
        // foreach (Employee employee in _employees)
        // {
        //     if (rule(employee))
        //     {
        //         result.Add(employee);
        //     }
        // }
        // return result;
        return _employees.Where(rule).ToList(); // Where asks the judge about each employee. ToList makes a real list
    }

    public List<Employee> GetSortedBySalary()
    {
        return _employees.OrderByDescending(e=>e.Salary).ToList();
    }
}