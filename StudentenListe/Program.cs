using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentenListe.src;

namespace StudentenListe
{
    class Program
    {
        static List<string> vornamen = new List<string>
        {
            "Sonya",
            "Susannah",
            "Khalilah",
            "Sal",
            "Modesto",
            "Allena",
            "Helga",
            "Thanh",
            "Mario",
            "Klara",
            "Lesley",
            "Eva",
            "Lindsey",
            "Shirly",
            "Suzy",
            "Olivia",
            "Mee",
            "Kym",
            "Suzi",
            "Gaye",
            "Summer",
            "Magdalen",
            "Julianne",
            "Keith",
            "Rosalinda",
            "Salvador",
            "William",
            "Chung",
            "Linnie",
            "Krissy",
            "Serena",
            "Elizabeth",
            "Seema",
            "Karin",
            "Wally",
            "Nancy",
            "Florentina",
            "Cathey",
            "Kathleen",
            "Marlo",
            "Fredricka",
            "Eddie",
            "Ezra",
            "Marine",
            "Kathe",
            "Shon",
            "Theo",
            "Glennis",
            "Connie",
            "Shari"
        };
        static List<string> nachnamen = new List<string>
        {
            "Hooper",
            "Hendrix",
            "Ho",
            "Burgess",
            "Stanley",
            "Barber",
            "Conley",
            "Dodson",
            "Arnold",
            "Hatfield",
            "Lopez",
            "Stephens",
            "Lloyd",
            "Delgado",
            "Gross",
            "Becker",
            "Mcmillan",
            "Aguirre",
            "Vaughn",
            "Sharp",
            "Stuart",
            "Bell",
            "Mahoney",
            "Lester",
            "Donovan",
            "Marshall",
            "Horton",
            "Melendez",
            "Merritt",
            "Cordova",
            "Sutton",
            "Pena",
            "Li",
            "Fitzgerald",
            "Arroyo",
            "Avila",
            "Wheeler",
            "Pineda",
            "Figueroa",
            "Riddle",
            "Strickland",
            "Fisher",
            "Villarreal",
            "Rodgers",
            "Rocha",
            "Norton",
            "Nielsen",
            "Holt",
            "Arellano",
            "Mckenzie"
        };
        static List<string> studiengaenge = new List<string>
        {
            "Angewandte Informatik",
            "Kommunikationsdesign",
            "BWL",
            "Biologie",
            "Chemie",
            "Physik",
            "Jura",
            "Medizin",
            "Museumskunde",
            "Metallbau",
            "Maschinenbau",
            "Geschichte"
        };

        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            Random rnd = new Random();
            //int id = rnd.Next(73628, 78329);
            int id = 0;
            for(int i = 0; i <= rnd.Next(10, 101); i++)
            {
                id = rnd.Next(73628, 78329);
                string vorname = vornamen[rnd.Next(0, vornamen.Count)];
                string nachname = nachnamen[rnd.Next(0, nachnamen.Count)];
                string studiengang = studiengaenge[rnd.Next(0, studiengaenge.Count)];

                Student neuerStudent = new Student(nachname, vorname, id, studiengang);
                StudentNode sn = new StudentNode(neuerStudent);

                if (i == 0)
                {
                    if (!linkedList.AddFirst(sn))
                    {
                        Console.WriteLine("Kopf hinzufügen fehlgeschlagen!");
                    }
                }
                else
                {
                    if (!linkedList.AddLast(sn))
                    {
                        Console.WriteLine("Element hinzufügen fehlgeschlagen!");
                        Console.WriteLine(sn.Student.ToString());
                    }
                }

                //id++;
            }
            linkedList.DeleteAt(5);
            linkedList.SelectionSortList();
            linkedList.OutputAll();

            Console.ReadKey();
        }


    }
}
