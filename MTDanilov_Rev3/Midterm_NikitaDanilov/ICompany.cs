using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_NikitaDanilov
{
    public interface ICompany : IComparable<ICompany>
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int JobCode { get; set; }
        string JobTitle { get; }
        DateTime DOB { get; set; }
        DateTime HireDate { get; set; }
        int YearsEmployed { get;  }
        decimal CurrentSalary { get; set; }
    }
}
