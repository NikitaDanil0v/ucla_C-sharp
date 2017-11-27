using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_ZooSorts_DanilovNikita
{
    public class Program
    {
        static void Main(string[] args)
        {
            Lion firstLion = new Lion("Leo", DateTime.Parse("4/04/2012"), 2500, 360, 1, 11);
            Lion secondLion = new Lion("George", DateTime.Parse("11/01/2009"), 2500, 380, 2, 11);
            Lion thirdLion = new Lion("Tom", DateTime.Parse("9/08/2011"), 2500, 400, 3, 11);
            Lion fourthLion = new Lion("Man-Eater", DateTime.Parse("7/28/2009"), 5000, 450, 4, 11);
            Lion fifthLion = new Lion("Kitty", DateTime.Parse("7/10/2011"), 2500, 450, 5, 17);
            Lion sixthLion = new Lion("Lord", DateTime.Parse("2/11/2008"), 2600, 470, 6, 18);
            Giraffe firstGiraffe = new Giraffe("Amber", DateTime.Parse("3/23/2013"), 4500, 1400, 7, 25);
            Giraffe secondGiraffe = new Giraffe("Charlie", DateTime.Parse("5/2/2012"), 3600, 2600, 8, 25);
            Giraffe thirdGiraffe = new Giraffe("Shirley", DateTime.Parse("3/23/2013"), 4500, 1400, 9, 25);
            Giraffe fourthGiraffe = new Giraffe("George", DateTime.Parse("5/2/2012"), 3600, 2600, 10, 25);
            Giraffe fifthGiraffe = new Giraffe("Tess", DateTime.Parse("1/23/2013"), 4500, 1400, 11, 25);
            Giraffe sixthGiraffe = new Giraffe("Carol", DateTime.Parse("5/2/2012"), 4500, 2600, 12, 25);

            List<IZoo> aZoo = new List<IZoo>();
            aZoo.Add(firstLion);
            aZoo.Add(secondLion);
            aZoo.Add(thirdLion);
            aZoo.Add(fourthLion);
            aZoo.Add(fifthLion);
            aZoo.Add(sixthLion);
            aZoo.Add(firstGiraffe);
            aZoo.Add(secondGiraffe);
            aZoo.Add(thirdGiraffe);
            aZoo.Add(fourthGiraffe);
            aZoo.Add(fifthGiraffe);
            aZoo.Add(sixthGiraffe);

            Console.WriteLine(Animal.SortTitle("Default"));
            aZoo.Sort();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in aZoo)
            {
                Console.WriteLine($"{animalData}");
            }

            // Implement a new sort by sorting the data by the purchase cost of the animal
            Console.WriteLine(Animal.SortTitle("Sorted by the purchase cost"));
            IZoo[] sortedP = aZoo.OrderBy(x => x.PurchaseCost).ToArray();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in sortedP)
            {
                Console.WriteLine($"{animalData}");
            }

            // Sort the data by the animal’s weight and date of birth
            Console.WriteLine(Animal.SortTitle("Sorted by the weight and date of birth"));
            IZoo[] sortedWDoB = aZoo.OrderBy(x => x.Weight).ThenBy(x => x.DOB).ToArray();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in sortedWDoB)
            {
                Console.WriteLine($"{animalData}");
            }

            // Sort the data by cage number, purchase cost, animal name, ID
            Console.WriteLine(Animal.SortTitle("Sorted by the cage number, purchase cost, animal name, ID"));
            IZoo[] sortedCPNID = aZoo.OrderBy(x => x.CageNumber).ThenBy(x => x.PurchaseCost).
                ThenBy(x => x.Name).ThenBy(x => x.ID).ToArray();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in sortedCPNID)
            {
                Console.WriteLine($"{animalData}");
            }

            // Sort the data by the animal’s age and then weight
            Console.WriteLine(Animal.SortTitle("Sorted by the animal’s age and then weight"));
            IZoo[] sortedAW = aZoo.OrderBy(x => x.Age).ThenBy(x => x.Weight).ToArray();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in sortedAW)
            {
                Console.WriteLine($"{animalData}");
            }

            // Sort the data by animal type(lion and giraffe), purchase cost, cage number, and name
            Console.WriteLine(Animal.SortTitle("Sorted by animal type(lion and giraffe), " +
                "purchase cost, cage number, and name"));
            IZoo[] sortedTPCN = aZoo.OrderBy(x => x.GetType().Name).
                ThenBy(x => x.PurchaseCost).ThenBy(x => x.CageNumber).ThenBy(x => x.Name).ToArray();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in sortedTPCN)
            {
                Console.WriteLine($"{animalData}");
            }

            // Sort the data by purchase cost, cage number, animal type, weight, age, name and ID
            Console.WriteLine(Animal.SortTitle("Sorted by purchase cost, cage number," +
                " animal type, weight, age, name and ID"));
            IZoo[] sortedPCTWANID = aZoo.OrderBy(x => x.PurchaseCost).ThenBy(x => x.CageNumber).
                ThenBy(x => x.GetType().Name).ThenBy(x => x.Weight).ThenBy(x => x.Age).
                ThenBy(x => x.Name).ThenBy(x => x.ID).ToArray();

            Console.WriteLine(PrintReportHeader());
            foreach (IZoo animalData in sortedPCTWANID)
            {
                Console.WriteLine($"{animalData}");
            }

            Console.WriteLine("\nPress <ENTER> to quit...");
            Console.ReadKey();
        }
        public static string PrintReportHeader() {
            return $"{"ID",-7} {"Animal Type",-15} {"Name",-15} {"Weight",-8}" +
                $"{"Age",-5} {"Purchase Cost",-16} {"Cage No.",-10}\n" +
                $"{"==",-7} {"===========",-15} {"====",-15} {"======",-8}" +
                $"{"===",-5} {"=============",-16} {"========",-10}";
        }
    }
}
