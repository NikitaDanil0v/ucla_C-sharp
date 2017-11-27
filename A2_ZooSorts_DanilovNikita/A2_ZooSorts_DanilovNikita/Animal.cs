using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_ZooSorts_DanilovNikita
{
    public class Animal
    {
        private int weight;
        private int age;
        private DateTime dob;

        public int Weight
        {
            get => weight;
            set { if (value > 0) weight = value; else weight = 0; }
        }
        public DateTime DOB
        {
            get => dob;
            set
            {
                if (value < DateTime.Now) dob = value; else dob = DateTime.MinValue; // signals unknown value
            }
        }
        public int GetAge()
        {
            DateTime currentDate = DateTime.Now;
            int years = currentDate.Year - DOB.Year;
            if ((currentDate.Month < DOB.Month) ||
                ((currentDate.Month == DOB.Month) &&
                (currentDate.Day < DOB.Day)))
            {
                --years;
            }
            return years;
        }
        public int Age
        {
            get => GetAge();
        }
        public static string SortTitle(string sortTitle)
        {
            return $"\nSort Type: {sortTitle}\n";
        }
        public override string ToString() { return $"{Weight,7} {GetAge(),4}"; }
    }
}
