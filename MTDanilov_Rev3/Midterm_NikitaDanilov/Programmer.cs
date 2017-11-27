using System;
using System.Data;
using System.Data.SqlClient;

namespace Midterm_NikitaDanilov
{
    public class Programmer : Employee, ICompany
    {
        #region Constructors
        public Programmer() : this(0, "Unknown", "Unknown", 0, DateTime.MinValue, DateTime.MinValue, 0)
        {
        }

        public Programmer(int empID, String firstName, String lastName, int jobCode, DateTime _DOB, DateTime _hireDate, decimal _currentSalary) :
            base(empID, firstName, lastName, jobCode, _DOB, _hireDate, _currentSalary)
        {
        }
        #endregion

        #region Properties
        public int CompletedTasks { get => CountCompletedTasks(); }
        public decimal Bonus { get => KPIBonus(); }
        #endregion

        private int CountCompletedTasks()
        {
            // Create Command object
            SqlCommand nonqueryCommand = SqlDatabase.sqlConnection.CreateCommand();

            // Create statement with named parameters
            nonqueryCommand.CommandText =
                "SELECT COUNT(*) FROM Tasks " +
                "WHERE OperatorName = @FirstName + ' ' + @LastName AND Status = 'Done'";

            // Add Parameters to Command Parameters collection
            nonqueryCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 10);
            nonqueryCommand.Parameters.Add("@LastName", SqlDbType.VarChar, 20);
 
            // Prepare command for repeated execution 
            nonqueryCommand.Prepare();

            nonqueryCommand.Parameters["@FirstName"].Value = this.FirstName;
            nonqueryCommand.Parameters["@LastName"].Value = this.LastName;

            return (int)nonqueryCommand.ExecuteScalar();
        }

        private decimal KPIBonus()
        {
            if (CompletedTasks > 3) { return CurrentSalary * (decimal)0.3; }
            if (CompletedTasks > 2) { return CurrentSalary * (decimal)0.2; }
            if (CompletedTasks > 1) { return CurrentSalary * (decimal)0.1; }
            return 0;
        }

        public static string ReportHeader()
        {
            Console.SetWindowSize(160, 20);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) { line += "="; };

            return line + $"Programmers" + "\n" + line +
                $"{"Job Code",10} {"Job Title",-20} {"Last Name",-12} {"First Name",-12} {"DOB",-12}" +
                $"{"Hire Date",-12} {"Salary",-12}" +
                $"{"Years Employed",-15} {"Completed Tasks",-17} {"Bonus",-6}";
        }

        public override string ToString()
        {
            return $"{JobCode,10} {JobTitle,-20} { LastName,-12} {FirstName,-12} {DOB,-12:MM/dd/yyyy}" +
                $"{ HireDate,-12:MM/dd/yyyy} {CurrentSalary,-12:C}" +
                $"{YearsEmployed,-15} {CompletedTasks,-17} {Bonus,-6:C}";
        }
    }
}
