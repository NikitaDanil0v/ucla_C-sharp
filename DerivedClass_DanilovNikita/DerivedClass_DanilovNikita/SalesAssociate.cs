using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivedClass_DanilovNikita
{
    class SalesAssociate: Employee
    {
        #region Fields
        private int _numberOfSales = 0;
        private decimal _bonus = 0;
        #endregion

        #region Constructors
        public SalesAssociate(): this(0, "Unknown", "Unknown", DateTime.MinValue, DateTime.MinValue, 0, 0)
        {
        }

        public SalesAssociate(int empID, String firstName, String lastName, DateTime _DOB, DateTime _hireDate, decimal _currentSalary, int numberOfSales):
            base(empID, firstName, lastName, _DOB, _hireDate, _currentSalary)
        {
            this.NumberOfSales = numberOfSales;
            this.SalesBonus();
        }
        #endregion

        #region Properties

        public int NumberOfSales { get => _numberOfSales; set => _numberOfSales = value; }
        public decimal Bonus { get => _bonus; set => _bonus = value; }
        #endregion


        //Adds a method named SalesBonus that calculates a bonus 5% if a Sales Associate makes
        //over 10 sales a month, 10% for over 20 sales, and 20% for over 30 sales per month.
        public void SalesBonus()
        {
            if (NumberOfSales > 10) { Bonus = CurrentSalary * (decimal) 0.05; }
            if (NumberOfSales > 20) { Bonus = CurrentSalary * (decimal) 0.1; }
            if (NumberOfSales > 30) { Bonus = CurrentSalary * (decimal) 0.2; }
        }


        public static string ReportHeader()
        {
            Console.SetWindowSize(120, 10);
            string line = String.Empty;
            for (int i = 0; i < Console.WindowWidth; i++) { line += "="; };

            return line + $"Sales Associate EIS" + "\n" + line +
                $"{"ID",5} {"Last Name",-25} {"First Name",-25} {"DOB",-13} " +
                $"{"Hire Date",-11} {"Salary",7} {"Bonus",9} {"Number of Sales",17}";
        }

        public override string ToString()
        {
            return $"{ID,5} {LastName,-25} {FirstName,-25} " +
                $"{DOB,-13:MM/dd/yyyy} {HireDate,-11:MM/dd/yyyy} " +
                $"{CurrentSalary,10:C} {Bonus,8:C} {NumberOfSales,15}";
        }
    }
}
