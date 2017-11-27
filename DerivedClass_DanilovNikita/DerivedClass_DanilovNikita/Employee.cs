using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivedClass_DanilovNikita
{
    public class Employee
    {
        #region Fields
        private int _ID = 0;
        private DateTime _DOB = DateTime.MinValue;
        private DateTime _hireDate = DateTime.MinValue;
        private decimal _currentSalary = 0;

        private static int empCount = 0;
        #endregion

        #region Constuctors
        public Employee() : this(0, "Unknown", "Unknown", DateTime.MinValue,
            DateTime.MinValue, 0)
        {
        }

        public Employee(int empID, String firstName, String lastName, DateTime _DOB, DateTime _hireDate, decimal _currentSalary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ID = empID;
            this.DOB = _DOB;
            this.HireDate = _hireDate;
            this.CurrentSalary = _currentSalary;

            empCount++;
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
        #endregion

        public int DaysEmployed()
        {
            DateTime hiredate = HireDate;
            DateTime currentDate = DateTime.Now;

            // Difference in days, hours and minutes
            TimeSpan ts = currentDate - hiredate;
            int differenceInDays = ts.Days;

            return differenceInDays;
        }

        public int YearsEmployed()
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


        public static string ReportHeader()
        {
            Console.SetWindowSize(120, 10);
            return $"Employee EIS\n" +
                $"{"ID",5} {"Last Name",-25} {"First Name",-25} {"DOB",-13}" +
                $"{"Hire Date",-11} {"Salary",8} {"Total Employees",15}";

        }
        public override string ToString()
        {
            return $"{ID,5} {LastName,-25} {FirstName,-25} " +
                $"{DOB,-11:MM/dd/yyyy} {HireDate,-11:MM/dd/yyyy} " +
                $"{CurrentSalary,10:C} {empCount,15}";
        }

    }
}
