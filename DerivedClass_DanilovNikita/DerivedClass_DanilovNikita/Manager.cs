using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivedClass_DanilovNikita
{
    public class Manager: Employee
    {
        #region Fields
        private int _stockOptions = 0;
        private decimal _bonus = 0;
        #endregion

        #region Constructors
        public Manager(): this(0, "Unknown", "Unknown", DateTime.MinValue, DateTime.MinValue, 0, 0, 0)
        {
        }

        public Manager(int empID, String firstName, String lastName, DateTime _DOB, DateTime _hireDate, decimal _currentSalary, decimal bonus, int stockOptions): 
            base(empID, firstName, lastName, _DOB, _hireDate, _currentSalary)
        {
            this.StockOptions = stockOptions; 
            this.Bonus = bonus;
        }
        #endregion

        #region Properties
        public int StockOptions { get => _stockOptions; set => _stockOptions = value; }
        public decimal Bonus { get => _bonus; set => _bonus = value; }
        #endregion

        public static string ReportHeader()
        {
            Console.SetWindowSize(120, 10);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) {line += "="; };

            return line + $"Manager EIS" + "\n" + line +
                $"{"ID",5} {"Last Name",-25} {"First Name",-25} {"DOB",-13} " +
                $"{"Hire Date",-11} {"Salary",7} {"Bonus",9} {"Stock Options",17}";
        }

        public override string ToString()
        {
            return $"{ID,5} {LastName,-25} {FirstName,-25} " +
                $"{DOB,-13:MM/dd/yyyy} {HireDate,-11:MM/dd/yyyy} " +
                $"{CurrentSalary,10:C} {Bonus,8:C} {StockOptions,15}";
        }

    }

}
