using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Getting Json object in collection
            IEnumerable<Owner> collection = GetObjects();

            // Getting male owner list using LINQ               
            IEnumerable<Owner> maleowners = collection.GroupBy(x => x.Gender).SelectMany(c => c).Where(x => x.Gender == "Male").ToList();

            // Getting female owner list using LINQ
            IEnumerable<Owner> femaleowners = collection.GroupBy(x => x.Gender).SelectMany(c => c).Where(x => x.Gender == "Female").ToList();

            // Getting male owner pets 'Cat' list using LINQ
            IEnumerable<Pet> maleownercats = maleowners.Select(x => x.Pets).Where(z => (z != null)).SelectMany(xx => xx).Where(pp => pp.Type == "Cat").OrderBy(p => p.Name).ToList();

            // Getting female owner pets 'Cat' list using LINQ
            IEnumerable<Pet> femaleownercats = femaleowners.Select(x => x.Pets).Where(z => (z != null)).SelectMany(xx => xx).Where(pp => pp.Type == "Cat").OrderBy(p => p.Name).ToList();
           
            Console.WriteLine("Male");
            Console.WriteLine();
            // Showing cat list for male owners in loop
            foreach (var i in maleownercats)
            {
                if (i.Name != null)
                {
                    Console.WriteLine(i.Name);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Female");
            Console.WriteLine();
            // Showing cat list for female owners in loop
            foreach (var i in femaleownercats)
            {
                if (i.Name != null)
                {
                    Console.WriteLine(i.Name);
                }
            }
                Console.ReadKey();
            }
        // Fetching Json object by using Newtonsoft.Json library
        private static IEnumerable<Owner> GetObjects()
        {
            List<Owner> result = new List<Owner>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://agl-developer-test.azurewebsites.net/people.json");
                result = JsonConvert.DeserializeObject<List<Owner>>(json);
            }
            return result;
        }
    }

    // Class for owner
    public class Owner
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }
    }

    // Class for pet
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

}
