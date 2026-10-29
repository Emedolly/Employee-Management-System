namespace EmployeeApp;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = new List<Employee>();
    public event Action<Employee> EmployeeAdded; //the doorbell
    public void Add(Employee employee)
    {
        _employees.Add(employee);
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
    public bool Remove(int id)
    {
        Employee employee = GetById(id);
        if (employee == null) return false;

        _employees.Remove(employee);
        return true;
    }

    public void Clear()
    {
        _employees.Clear();
    }
}