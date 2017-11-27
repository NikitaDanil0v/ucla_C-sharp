using System;
using System.Collections.Generic;

namespace Midterm_NikitaDanilov
{
    class Program
    {
        //********************************************************************************************
        // Author: Nikita Danilov
        // Logic:
        //   Database connection wrapperd to singleton. Each employee class created 
        //   with 'CreateInstance' for better readability and flexability.
        //
        // Database Structure:
        // - Employees (List of all employees in the Company)
        // - Sales (List of all sales with the name of sales associate - agent name)
        // - Tasks (List of all tasks with the name of programmer/associate programer - operator name)
        // - Reimbursements (List of all reimbursements with certain budget)
        // - dictPositions (Dictionary for Job Titles by Job Code)
        // - dictAgents (Dictionary for Agents by Sales Managers and Divisions)
        //********************************************************************************************

        private static string namespaceName = "Midterm_NikitaDanilov";
        private static string App_Data = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\";

        static void Main(string[] args)
        {   
            // Setting up App_Data directory
            AppDomain.CurrentDomain.SetData("DataDirectory", App_Data);

            try
            {
                // Open connection to the database
                SqlDatabase.Instance.OpenConnection();

                // Employee Information System for the Great Corporation
                Console.WriteLine("Employee Information System for the Great Corporation");
                List<ICompany> Company = new List<ICompany>();


                // Associate Programmer
                Console.WriteLine(Type.GetType(namespaceName + "." + "AssociateProgrammer")
                    .GetMethod("ReportHeader").Invoke(null, null));

                foreach (var empParams in RetrieveData("Associate Programmer"))
                {
                    var employee = (AssociateProgrammer)Activator.CreateInstance(
                        Type.GetType(namespaceName + "." + "AssociateProgrammer"), empParams);
                    Console.WriteLine(employee);
                    Company.Add(employee);
                }

                // Programmer
                Console.WriteLine(Type.GetType(namespaceName + "." + "Programmer")
                    .GetMethod("ReportHeader").Invoke(null, null));

                foreach (var empParams in RetrieveData("Programmer"))
                {
                    var employee = (Programmer)Activator.CreateInstance(
                        Type.GetType(namespaceName + "." + "Programmer"), empParams);
                    Console.WriteLine(employee);
                    Company.Add(employee);
                }

                // Sales Associate
                Console.WriteLine(Type.GetType(namespaceName + "." + "SalesAssociate")
                    .GetMethod("ReportHeader").Invoke(null, null));

                foreach (var empParams in RetrieveData("Sales Associate"))
                {
                    var employee = (SalesAssociate)Activator.CreateInstance(
                        Type.GetType(namespaceName + "." + "SalesAssociate"), empParams);
                    Console.WriteLine(employee);
                    Company.Add(employee);
                }

                // Sales Manager
                Console.WriteLine(Type.GetType(namespaceName + "." + "SalesManager")
                    .GetMethod("ReportHeader").Invoke(null, null));

                foreach (var empParams in RetrieveData("Sales Manager"))
                {
                    var employee = (SalesManager)Activator.CreateInstance(
                        Type.GetType(namespaceName + "." + "SalesManager"), empParams);
                    Console.WriteLine(employee);
                    Company.Add(employee);
                }

                // President
                Console.WriteLine(Type.GetType(namespaceName + "." + "President")
                    .GetMethod("ReportHeader").Invoke(null, null));

                foreach (var empParams in RetrieveData("President"))
                {
                    var employee = (President)Activator.CreateInstance(
                        Type.GetType(namespaceName + "." + "President"), empParams);
                    Console.WriteLine(employee);
                    Company.Add(employee);
                }

                // Sort by job position, current salary, and name
                Company.Sort();

                // Print all employees
                Console.WriteLine(Type.GetType(namespaceName + "." + "Employee")
                    .GetMethod("ReportHeader").Invoke(null, null));
                foreach (ICompany empData in Company)
                {
                    Console.WriteLine($"{empData}");
                }
            }
            finally
            {
                // Close connection to the database
                SqlDatabase.Instance.CloseConnection();
            }
            Console.WriteLine("\nPress <ENTER> to quit...");
            Console.ReadKey();
        }

        // Retrieving Data by Job Title using Job Code from Positions dictionary
        private static List<object[]> RetrieveData(String jobTitle)
        {
            return SqlDatabase.Instance.SelectData(
                    "SELECT " +
                        "t1.EmployeeId, " +
                        "t1.FirstName, " +
                        "t1.LastName, " +
                        "t1.JobCode, " +
                        "t1.DateOfBirth, " +
                        "t1.HireDate, " +
                        "t1.CurrentSalary " +
                    "FROM Employees t1 " +
                    "LEFT JOIN dictPositions t2 " +
                    "ON t1.JobCode = t2.JobCode " +
                    "WHERE t2.JobTitle = '" + jobTitle + "'"
                    );
        }
    }
}
