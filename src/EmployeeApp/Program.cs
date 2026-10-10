// See https://aka.ms/new-console-template for more information

using EmployeeApp;

List<Employee> employees = new List<Employee>();
employees.Add(new Employee(1, "A", "RD", 123));
employees.Add(new Employee(2, "B", "RD1", 1234));
employees.Add(new Employee(3, "C", "RD2", 1235));
foreach (Employee employee in employees)
{
    Console.WriteLine(employee.Name);
}
EmployeeService service = new EmployeeService();
while (true)
{
    Console.WriteLine("Enter an ID:");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
    }
}
Console.WriteLine("Hello, World!");