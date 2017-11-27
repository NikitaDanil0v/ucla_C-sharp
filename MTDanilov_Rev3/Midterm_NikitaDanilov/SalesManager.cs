using System;
using System.Data;
using System.Data.SqlClient;

namespace Midterm_NikitaDanilov
{
    public class SalesManager : Employee, ICompany
    {
        #region Constructors
        public SalesManager() : this(0, "Unknown", "Unknown", 0, DateTime.MinValue, DateTime.MinValue, 0)
        {
        }

        public SalesManager(int empID, String firstName, String lastName, int jobCode, DateTime _DOB, DateTime _hireDate, decimal _currentSalary) :
            base(empID, firstName, lastName, jobCode, _DOB, _hireDate, _currentSalary)
        {
        }
        #endregion

        #region Properties

        public decimal SalesTotalAmount { get => SumTotalSales(); }
        public decimal CarAllowance { get => GetCarAllowance(); } 
        public decimal Bonus { get => SalesBonus(); }
        #endregion

        // For Sales Manager hold a similar percent
        // but his/her bonus is based upon total sales for his/her division
        private decimal SumTotalSales()
        {
            // Create Command object
            SqlCommand nonqueryCommand = SqlDatabase.sqlConnection.CreateCommand();

            // Create statement with named parameters
            nonqueryCommand.CommandText =
                "SELECT SUM(ContractPrice) FROM Sales t1 " +
                "INNER JOIN dictAgents t2 " +
                "ON t1.AgentName = t2.AgentName AND t1.Status = 'Done'" +
                "WHERE t2.ManagerName = @FirstName + ' ' + @LastName";

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

        // Sales Manager and President would also
        // have a car allowance field that would hold a certain amount of money for the use of a car
        private decimal GetCarAllowance()
        {
            // Create Command object
            SqlCommand nonqueryCommand = SqlDatabase.sqlConnection.CreateCommand();

            // Create statement with named parameters
            nonqueryCommand.CommandText =
                "SELECT Budget FROM Reimbursements " +
                "WHERE EmployeeName = @FirstName + ' ' + @LastName";
                

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
            if (SalesTotalAmount > 30000) { return CurrentSalary * (decimal)0.2; }
            if (SalesTotalAmount > 20000) { return CurrentSalary * (decimal)0.1; }
            if (SalesTotalAmount > 10000) { return CurrentSalary * (decimal)0.05; }
            return 0;
        }

        public static string ReportHeader()
        {
            Console.SetWindowSize(160, 20);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) { line += "="; };

            return line + $"Sales Managers" + "\n" + line +
                $"{"Job Code",10} {"Job Title",-20} {"Last Name",-12} {"First Name",-12} {"DOB",-12}" +
                $"{"Hire Date",-12} {"Salary",-12}" +
                $"{"Years Employed",-15} {"Sales Total Amount",-17} {"Bonus",-11} {"Car Allowance",-14}";
        }

        public override string ToString()
        {
            return $"{JobCode,10} {JobTitle,-20} {LastName,-12} {FirstName,-12} {DOB,-12:MM/dd/yyyy}" +
                $"{ HireDate,-12:MM/dd/yyyy} {CurrentSalary,-12:C}" +
                $"{YearsEmployed,-15} {SalesTotalAmount,-17:C} {Bonus,-11:C} {CarAllowance,-14:C}";
        }
    }
}
