namespace EmployeeApp;

public interface IEmployeeRepository
{
    void Add(Employee employee);
    List<Employee> GetAll();
    Employee GetById(int id);
    bool Remove(int id);
    void Clear();
}