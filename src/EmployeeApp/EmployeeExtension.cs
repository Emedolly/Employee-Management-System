namespace EmployeeApp;

public static class EmployeeExtension
{
    public static string ToCsvLine(this Employee employee)
    {
        return $"{employee.Id},{employee.Name},{employee.Department},{employee.Salary}";
    }

    public static string ToDisplay(this Employee employee)
    {
        return $"{employee.Id} | {employee.Name} | {employee.Department} | {employee.Salary}";
    }
}