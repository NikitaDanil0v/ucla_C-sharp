using System;
using System.Data;
using System.Data.SqlClient;

namespace Midterm_NikitaDanilov
{
    public class SalesAssociate: Employee, ICompany
    {

        #region Constructors
        public SalesAssociate() : this(0, "Unknown", "Unknown", 0, DateTime.MinValue, DateTime.MinValue, 0)
        {
        }

        public SalesAssociate(int empID, String firstName, String lastName, int jobCode, DateTime _DOB, DateTime _hireDate, decimal _currentSalary) :
            base(empID, firstName, lastName, jobCode, _DOB, _hireDate, _currentSalary)
        {
        }
        #endregion

        #region Properties

        public decimal SalesAmount { get => SumSales(); }
        public decimal Bonus { get => SalesBonus(); }
        #endregion

        // For sales associates also hold a bonus percent and a dollar amount for the sales made
        // by that associate during a sales time period
        private decimal SumSales()
        {
            // Create Command object
            SqlCommand nonqueryCommand = SqlDatabase.sqlConnection.CreateCommand();

            // Create statement with named parameters
            nonqueryCommand.CommandText =
                "SELECT SUM(ContractPrice) FROM Sales " +
                "WHERE AgentName = @FirstName + ' ' + @LastName AND Status = 'Done'";

            // Add Parameters to Command Parameters collection
            nonqueryCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 10);
            nonqueryCommand.Parameters.Add("@LastName", SqlDbType.VarChar, 20);

            // Prepare command for repeated execution 
            nonqueryCommand.Prepare();

            nonqueryCommand.Parameters["@FirstName"].Value = this.FirstName;
            nonqueryCommand.Parameters["@LastName"].Value = this.LastName;

            var result = nonqueryCommand.ExecuteScalar();
            if (result != DBNull.Value) return (decimal)result;
            return 0;
        }

        private decimal SalesBonus()
        {
            if (SalesAmount > 30000) { return CurrentSalary * (decimal)0.2; }
            if (SalesAmount > 20000) { return CurrentSalary * (decimal)0.1; }
            if (SalesAmount > 10000) { return CurrentSalary * (decimal)0.05; }
            return 0;
        }

        public static string ReportHeader()
        {
            Console.SetWindowSize(160, 20);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) { line += "="; };

            return line + $"Sales Associate" + "\n" + line +
                $"{"Job Code",10} {"Job Title",-20} {"Last Name",-12} {"First Name",-12} {"DOB",-12}" +
                $"{"Hire Date",-12} {"Salary",-12}" +
                $"{"Years Employed",-15} {"Sales Amount",-17} {"Bonus",-6}";
        }

        public override string ToString()
        {
            return $"{JobCode,10} {JobTitle,-20} { LastName,-12} {FirstName,-12} {DOB,-12:MM/dd/yyyy}" +
                $"{ HireDate,-12:MM/dd/yyyy} {CurrentSalary,-12:C}" +
                $"{YearsEmployed,-15} {SalesAmount,-17:C} {Bonus,-6:C}";
        }
    }
}
