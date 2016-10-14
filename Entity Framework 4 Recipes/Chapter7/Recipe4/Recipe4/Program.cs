using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
namespace Recipe4
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = PluralizationService.CreateService(new CultureInfo("en-US"));
            string person = "Person";
            string people = "People";
            Console.WriteLine("The plural of {0} is {1}", person, service.Pluralize(person));
            Console.WriteLine("The singular of {0} is {1}", people, service.Singularize(people));

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
