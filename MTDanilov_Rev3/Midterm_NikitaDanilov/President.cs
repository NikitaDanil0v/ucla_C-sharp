using System;
using System.Data;
using System.Data.SqlClient;

namespace Midterm_NikitaDanilov
{
    public class President : Employee, ICompany
    {
        #region Constructors
        public President() : this(0, "Unknown", "Unknown", 0, DateTime.MinValue, DateTime.MinValue, 0)
        {
        }

        public President(int empID, String firstName, String lastName, int jobCode, DateTime _DOB, DateTime _hireDate, decimal _currentSalary) :
            base(empID, firstName, lastName, jobCode, _DOB, _hireDate, _currentSalary)
        {
        }
        #endregion

        #region Properties
        public decimal CarAllowance { get => GetCarAllowance(); }
        #endregion

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

        public static string ReportHeader()
        {
            Console.SetWindowSize(160, 20);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) { line += "="; };

            return line + $"President" + "\n" + line +
                $"{"Job Code",10} {"Job Title",-20} {"Last Name",-12} {"First Name",-12} {"DOB",-12}" +
                $"{"Hire Date",-12} {"Salary",-12}" +
                $"{"Years Employed",-15} {"Car Allowance",-14}";
        }

        public override string ToString()
        {
            return $"{JobCode,10} {JobTitle,-20} { LastName,-12} {FirstName,-12} {DOB,-12:MM/dd/yyyy}" +
                $"{ HireDate,-12:MM/dd/yyyy} {CurrentSalary,-12:C}" +
                $"{YearsEmployed,-15} {CarAllowance,-14:C}";
        }
    }
}
