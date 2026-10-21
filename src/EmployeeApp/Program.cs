// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Xml.Linq;
using EmployeeApp;

string filepath = "employees.txt";
EmployeeService service = new EmployeeService();
service.EmployeeAdded += emp => Console.WriteLine($"Notification:{emp.Name} was added"); // subscribe ONCE
service.LoadFromFile(filepath); // data survives closing the app
bool running = true;
while (running)
{
    PrintMenu();
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1": AddEmployee(service); break;
        case "2": ViewEmployee(service); break;
        case "3": SearchEmployee(service); break;
        case "4": UpdateEmployee(service); break;
        case "5": DeleteEmployee(service); break;
        case "6": CalculateSalary(service); break;
        case "7": Save(service, filepath); break;
        case "8": Load(service, filepath); break;
        case "9": FilterEmployees(service); break;
        case "0": running = false; break;
        default:
            Console.WriteLine("Invalid choice"); break;
    }

    static void PrintMenu()
    {
        Console.WriteLine("1.Add");
        Console.WriteLine("2.View");
        Console.WriteLine("3.Search");
        Console.WriteLine("4.Update");
        Console.WriteLine("5.Delete");
        Console.WriteLine("6.CalculateSalary:");
        Console.WriteLine("7:Save to File");
        Console.WriteLine("8.Load from File");
        Console.WriteLine("9.Filter");
        Console.WriteLine("0.Exit");
        Console.Write("Choose: ");
    }

    static bool ReadInt(string prompt, out int value)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out value)) return true;
        Console.WriteLine("Invalid input");
        return false;
    }

    static bool ReadDecimal(string prompt, out decimal value)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out value)) return true;
        Console.WriteLine("Invalid input");
        return false;
    }

    static void PrintList(List<Employee> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("No employees");
            return;
        }

        foreach (var emp in list)
        {
            Console.WriteLine(emp.ToDisplay());
        }
    }

    static void AddEmployee(EmployeeService service)
    {
        if (!ReadInt("Enter ID: ", out int id)) return;

        Console.WriteLine("Enter name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter department:");
        string department = Console.ReadLine();

        Console.WriteLine("Enter salary:");

        if (!ReadDecimal("Enter Salary: ", out decimal salary)) return;
        try
        {
            Employee employee = new Employee(id, name, department, salary);
            service.AddEmployee(employee);
            Console.WriteLine("Employee added");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    static void ViewEmployee(EmployeeService service)
    {
        PrintList(service.GetAll());
    }

    static void SearchEmployee(EmployeeService service)
    {
        if (!ReadInt("Enter ID: ", out int id)) return;
        Employee employee = service.GetById(id);
        if (employee != null)
        {
            //Console.WriteLine($"{employee.Id} | {employee.Name} | {employee.Salary}");
            employee.ToDisplay();
        }
        else
        {
            Console.WriteLine("Employee not found");
        }
    }

    static void UpdateEmployee(EmployeeService service)
    {
        if (!ReadInt("Enter ID: ", out int id)) return;
        Console.Write("Enter new name: ");
        string name = Console.ReadLine();

        Console.Write("Enter new department: ");
        string department = Console.ReadLine();

        if (!ReadDecimal("Enter new salary: ", out decimal salary)) return;

        try
        {
            bool updated = service.UpdateById(id, name, department, salary);
            Console.WriteLine(updated ? "Updated" : "Id not found");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeleteEmployee(EmployeeService service)
    {
        if (!ReadInt("Enter Id to delete: ", out int id)) return;

        bool deleted = service.DeleteById(id);
        Console.WriteLine(deleted ? "Employee deleted" : "Id not found");
    }

    static void CalculateSalary(EmployeeService service)
    {
        if (!ReadInt("Enter Id: ", out int id)) return;

        try
        {
            decimal net = service.CalculateNetSalary(id);
            Console.WriteLine($"Net salary: {net}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message); // "Employee not found"
        }
    }

    static void Save(EmployeeService service, string filePath)
    {
        try
        {
            service.SaveToFile(filePath);
            Console.WriteLine("Saved");
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void Load(EmployeeService service, string filePath)
    {
        try
        {
            service.LoadFromFile(filePath);
            Console.WriteLine("Loaded");
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void FilterEmployees(EmployeeService service)
    {
        Console.WriteLine("1. By department");
        Console.WriteLine("2. Salary above amount");
        Console.Write("Choose: ");
        string filterChoice = Console.ReadLine();

        if (filterChoice == "1")
        {
            Console.Write("Enter department: ");
            string dept = Console.ReadLine();
            PrintList(service.Filter(e => e.Department == dept)); // judge: department matches
        }
        else if (filterChoice == "2")
        {
            if (!ReadDecimal("Enter amount: ", out decimal amount)) return;
            PrintList(service.Filter(e => e.Salary > amount)); // judge: salary bigger
        }
        else
        {
            Console.WriteLine("Wrong choice");
        }
    }
    // case "5":
    // {
    //     running = false;
    //     break;
    // }
    // case "6":
    // {
    //     Console.WriteLine("Enter id to update:");
    //     int id = int.Parse(Console.ReadLine());
    //     service.UpdateById(id, "Emeema", "ICandD", 30000);
    //     break;
    // }
    // case "7":
    // {
    //     Console.WriteLine("Enter id to calculate:");
    //     int id = int.Parse(Console.ReadLine());
    //     decimal result = service.CalculateNetSalary(id);
    //     Console.WriteLine($"Salary: {result}");
    //     break;
    // }
    // case "8":
    // {
    //     string path = "/Users/emeema/Desktop/text/employee.csv";
    //     service.SaveToFile(path);
    //     Console.WriteLine("Employee saved");
    //     break;
    // }
    // case "9":
    // {
    //     string path = "/Users/emeema/Desktop/text/employee.csv";
    //     service.LoadFromFile(path);
    //     foreach (Employee emp in service.GetAll())
    //     {
    //         Console.WriteLine($"{emp.Id} | {emp.Name} | {emp.Department} | {emp.Salary}");
    //     }
    //
    //     break;
    // }
    // case "10":
    // {
    //     Console.WriteLine("1.By department");
    //     Console.WriteLine("2.By Salary above amount");
    //     Console.Write("choose:");
    //     List<Employee> matches = new List<Employee>();
    //     string filterChoice = Console.ReadLine();
    //     if (filterChoice == "1")
    //     {
    //         Console.WriteLine("Enter department:");
    //         string department = Console.ReadLine();
    //         matches = service.Filter(e => e.Department == department);
    //     }
    //     else if (filterChoice == "2")
    //     {
    //         Console.WriteLine("Enter salary:");
    //         if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
    //         {
    //             Console.WriteLine("Invalid input");
    //             break;
    //         }
    //
    //         matches = service.Filter(e => e.Salary == salary);
    //     }
    //     else
    //     {
    //         Console.WriteLine("Invalid input");
    //     }
    //
    //     foreach (Employee emp in matches)
    //     {
    //         Console.WriteLine(emp.ToDisplay());
    //     }
    //
    //     break;
    // }
}