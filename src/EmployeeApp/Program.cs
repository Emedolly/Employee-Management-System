// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using EmployeeApp;

EmployeeService service = new EmployeeService();
bool running = true;
while (running)
{
    Console.WriteLine("1.Add");
    Console.WriteLine("2.View");
    Console.WriteLine("3.Search");
    Console.WriteLine("4.Delete");
    Console.WriteLine("5.Exit");
    Console.WriteLine("6:Update");
    Console.WriteLine("7:CalculateSalary");
    Console.WriteLine("8.Save to File");
    Console.WriteLine("9.Load from File");
    string input = Console.ReadLine();
    switch (input)
    {
        case "1":
        {
            Console.WriteLine("Enter iD:");
            //int id = int.Parse(Console.ReadLine());
            if (!int.TryParse(Console.ReadLine(), out int id)) // not a number? TryParse gives false
            {
                Console.WriteLine("Invalid number");
                break; // leave the switch, the while loop shows the menu again
            }
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter department:");
            string department = Console.ReadLine();
            Console.WriteLine("Enter salary:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
            {
                Console.WriteLine("Invalid input");
            }
            Employee employee = new Employee(id, name, department, salary);
            service.AddEmployee(employee);
            Console.WriteLine("Employee added");
            break;
        }
        case "2":
        {
            List<Employee> result = new List<Employee>();
            result = service.GetAll();
            foreach (Employee emp in result)
            {
                Console.WriteLine($"{emp.Id} | {emp.Name} | {emp.Department} | {emp.Salary}");
            }
            break;
        }
        case "3":
        {
            Console.WriteLine("Enter id:");
            int id = int.Parse(Console.ReadLine());
            Employee employee = service.GetById(id);
            if (employee != null)
            {
                Console.WriteLine($"{employee.Id} | {employee.Name} | {employee.Salary}");
            }
            else
            {
                Console.WriteLine("Employee not found");
            }
            break;
        }
        case "4":
        {
            Console.WriteLine("Enter iD TO DELETE:");
            int id = int.Parse(Console.ReadLine());
            bool result = service.DeleteById(id);
            if (result)
            {
                Console.WriteLine("Employee deleted");
            }
            break;
        }
        case "5":
        {
            running = false;
            break;
        }
        case "6":
        {
           Console.WriteLine("Enter id to update:");
           int id = int.Parse(Console.ReadLine());
           service.UpdateById(id, "Emeema", "ICandD", 30000);
           break;
        }
        case "7":
        {
            Console.WriteLine("Enter id to calculate:");
            int id = int.Parse(Console.ReadLine());
            decimal result = service.CalculateNetSalary(id);
            Console.WriteLine($"Salary: {result}");
            break;
        }
        case "8":
        {
            string path = "/Users/emeema/Desktop/text/employee.csv";
            service.SaveToFile(path);
            Console.WriteLine("Employee saved");
            break;
        }
        case "9":
        {
            string path = "/Users/emeema/Desktop/text/employee.csv";
            service.LoadFromFile(path);
            foreach (Employee emp in service.GetAll())
            {
                Console.WriteLine($"{emp.Id} | {emp.Name} | {emp.Department} | {emp.Salary}");
            }
            break;
        }
    }
}