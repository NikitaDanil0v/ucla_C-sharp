using System;
using System.Data;
using System.Data.SqlClient;

namespace Midterm_NikitaDanilov
{
    public class Employee
    {
        #region Fields
        private int _ID = 0;
        private DateTime _DOB = DateTime.MinValue;
        private DateTime _hireDate = DateTime.MinValue;
        private decimal _currentSalary = 0;
        #endregion

        #region Constuctors
        public Employee() : this(0, "Unknown", "Unknown", 0, DateTime.MinValue,
            DateTime.MinValue, 0)
        {
        }

        public Employee(int empID, String firstName, String lastName, int jobCode, DateTime _DOB, DateTime _hireDate, decimal _currentSalary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.JobCode = jobCode;
            this.ID = empID;
            this.DOB = _DOB;
            this.HireDate = _hireDate;
            this.CurrentSalary = _currentSalary;
        }
        #endregion

        #region Properties
        public int ID { get => _ID; set => _ID = value; }
        public DateTime DOB
        {
            get => _DOB;
            set
            {
                if (value <= DateTime.Now)
                    _DOB = value;
                else
                    throw new ArgumentOutOfRangeException("DOB",
                        "Date of birth must be <= current date...");
                _DOB = value;
            }
        }
        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                if (value <= DateTime.Now)
                    _hireDate = value;
                else
                    throw new ArgumentOutOfRangeException("HireDate",
                        "Hire date must be <= current date...");
                _hireDate = value;
            }
        }
        public decimal CurrentSalary
        {
            get => _currentSalary;
            set
            {
                if (value >= 0) _currentSalary = value;
                else throw new ArgumentOutOfRangeException("CurrentSalary",
                    "Current salary must be >= 0");
                _currentSalary = value;
            }
        }

        public String FirstName { get; set; } = String.Empty;
        public String LastName { get; set; } = String.Empty;
        public int JobCode { get; set; } = 0;
        public String JobTitle { get => GetJobTitle(); }
        #endregion

        private String GetJobTitle()
        {
            // Create Command object
            SqlCommand nonqueryCommand = SqlDatabase.sqlConnection.CreateCommand();

            // Create statement with named parameters
            nonqueryCommand.CommandText =
                "SELECT JobTitle FROM dictPositions " +
                "WHERE JobCode = @JobCode";

            // Add Parameters to Command Parameters collection
            nonqueryCommand.Parameters.Add("@JobCode", SqlDbType.Int);

            // Prepare command for repeated execution 
            nonqueryCommand.Prepare();

            nonqueryCommand.Parameters["@JobCode"].Value = this.JobCode;

            return (string) nonqueryCommand.ExecuteScalar();
        }

        public int YearsEmployed
        {
            get
            {
                DateTime currentDate = DateTime.Now;
                int years = currentDate.Year - HireDate.Year;

                if ((currentDate.Month == HireDate.Month) ||
                    ((currentDate.Month == HireDate.Month) &&
                    (currentDate.Day < HireDate.Day)))
                {
                    --years;
                }
                return years;
            }
        }

        public int CompareTo(ICompany other)
        {
            // return -1 if this is less than other
            // return 0 if this == other
            // return 1 if this > other
            // note that other can never be null because we
            // are using an instance method. Thus you must test for a null condition.
            // Note: most developers treat a null other condition as this > other
            if (other == null)
            {
                return 1; // i.e. this is greater than other (null) 
            }
            int compareValue = 0;

            compareValue = JobTitle.CompareTo(other.JobTitle);
            if (compareValue == 0)
            {
                compareValue = CurrentSalary.CompareTo(other.CurrentSalary);
                if (compareValue == 0) { compareValue = FirstName.CompareTo(other.FirstName); }
            }
            return compareValue;
        }

        public static string ReportHeader()
        {
            Console.SetWindowSize(160, 20);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) { line += "="; };

            return line + $"Employees" + "\n" + line +
                $"{"Job Code",10} {"Job Title",-20} {"Last Name",-12} {"First Name",-12} {"DOB",-12}" +
                $"{"Hire Date",-12} {"Salary",-12}" +
                $"{"Years Employed",-15}";
        }

        public override string ToString()
        {
            return $"{JobCode,10} {JobTitle,-20} { LastName,-12} {FirstName,-12} {DOB,-12:MM/dd/yyyy}" +
                $"{ HireDate,-12:MM/dd/yyyy} {CurrentSalary,-12:C}" +
                $"{YearsEmployed,-15}";
        }
    }
}
