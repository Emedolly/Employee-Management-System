namespace EmployeeApp;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;       // typed as the INTERFACE, not the class
    public event Action<Employee> EmployeeAdded;

    public EmployeeService(IEmployeeRepository repository)  // the plug arrives here (constructor injection)
    {
        _repository = repository;
    }

    public void AddEmployee(Employee employee)
    {
        if (_repository.GetById(employee.Id) != null)       // business rule stays in the service
            throw new ArgumentException("Id already exists");

        _repository.Add(employee);
        EmployeeAdded?.Invoke(employee);                    // ring the bell LAST
    }

    public List<Employee> GetAll()
    {
        return _repository.GetAll();
    }

    public Employee GetById(int id)
    {
        return _repository.GetById(id);
    }

    public bool DeleteById(int id)
    {
        return _repository.Remove(id);
    }

    public bool UpdateById(int id, string name, string department, decimal salary)
    {
        Employee employee = _repository.GetById(id);
        if (employee == null) return false;

        employee.Salary = salary;                           // FIRST: a bad salary stops before any change
        employee.Name = name;
        employee.Department = department;
        return true;
    }

    public decimal CalculateNetSalary(int id)
    {
        Employee employee = _repository.GetById(id);
        if (employee == null)
            throw new ArgumentException("Employee not found");

        decimal bonus = employee.Salary * 0.10m;
        decimal tax = employee.Salary * 0.05m;
        return employee.Salary + bonus - tax;
    }

    public void SaveToFile(string path)
    {
        List<string> lines = new List<string>();
        foreach (Employee employee in _repository.GetAll())
        {
            lines.Add(employee.ToCsvLine());
        }
        File.WriteAllLines(path, lines);
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path)) return;

        string[] lines = File.ReadAllLines(path);
        _repository.Clear();                                // no duplicates on reload

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length != 4) continue;
            if (!int.TryParse(parts[0], out int id)) continue;
            if (!decimal.TryParse(parts[3], out decimal salary)) continue;
            if (salary < 0) continue;

            _repository.Add(new Employee(id, parts[1], parts[2], salary));
        }
    }

    public List<Employee> Filter(Func<Employee, bool> rule)
    {
        return _repository.GetAll().Where(rule).ToList();
    }

    public List<Employee> GetSortedBySalary()
    {
        return _repository.GetAll().OrderByDescending(e => e.Salary).ToList();
    }

    public Employee GetHighestPaid()
    {
        return _repository.GetAll().OrderByDescending(e => e.Salary).FirstOrDefault();
    }

    public decimal GetTotalSalary()
    {
        return _repository.GetAll().Sum(e => e.Salary);
    }

    public Dictionary<string, int> GetCountByDepartment()
    {
        return _repository.GetAll()
            .GroupBy(e => e.Department)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}