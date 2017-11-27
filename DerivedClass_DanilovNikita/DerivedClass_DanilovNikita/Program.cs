using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivedClass_DanilovNikita
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Manager.ReportHeader());
            Manager manager = new Manager();
            Console.WriteLine(manager);

            Console.WriteLine(SalesAssociate.ReportHeader());
            SalesAssociate salesAssociate = new SalesAssociate();
            Console.WriteLine(salesAssociate);


            List<Employee> employees = new List<Employee>();
            employees.Add(new Manager(102, "Carol", "Smith",
                DateTime.Parse("2/23/1993"), DateTime.Parse("9/5/2011"), 7580M, 500, 50));
            employees.Add(new Manager(103, "Bill", "Goode",
                DateTime.Parse("1/11/2004"), DateTime.Parse("3/12/2012"), 5600M, 0 ,0));
            employees.Add(new SalesAssociate(34, "Homer", "Grant",
                DateTime.Parse("5/1/1987"), DateTime.Parse("2/2/2011"), 4300M, 15));
            employees.Add(new SalesAssociate(211, "Elena", "Garcia",
                DateTime.Parse("4/4/1995"), DateTime.Parse("3/24/2007"), 3400M, 5));

            string prev_typeName = String.Empty;
            string typeName = String.Empty;
            foreach (Employee employee in employees)
            {
                typeName = employee.GetType().Name;
                if (typeName != prev_typeName)
                {
                    Console.WriteLine(Type.GetType(employee.GetType().ToString()).GetMethod("ReportHeader").Invoke(null, null));
                }
                prev_typeName = typeName;

                Console.WriteLine(employee);
            }
            
            Console.WriteLine("\nPress <Enter> to quit...");
            Console.ReadKey();
        }
    }
}
