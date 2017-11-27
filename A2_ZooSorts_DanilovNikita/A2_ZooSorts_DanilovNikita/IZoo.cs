using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_ZooSorts_DanilovNikita
{
    public interface IZoo: IComparable<IZoo>
    {
        int ID { get; set; }
        string Name { get; set; }
        int Weight { get; set; }
        DateTime DOB { get; set; }
        int Age { get; }
        decimal PurchaseCost { get; set; }
        int CageNumber { get; set; }
    }
}
