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

        private static LinkedList studentLinkedList;

        static void Main(string[] args)
        {
            Console.WriteLine("Sie haben das Studentenlistenprogramm gestartet.");
            bool continueProgram = true;
            while (continueProgram)
            {
                continueProgram = mainmenu();
            }
        }

        private static void GenerateRandomList()
        {
            LinkedList linkedList = new LinkedList();
            Random rnd = new Random();
            //int id = rnd.Next(73628, 78329);
            int id = 0;
            for (int i = 0; i <= rnd.Next(10, 101); i++)
            {
                id = rnd.Next(73628, 78329);
                string vorname = vornamen[rnd.Next(0, vornamen.Count)];
                string nachname = nachnamen[rnd.Next(0, nachnamen.Count)];
                string studiengang = studiengaenge[rnd.Next(0, studiengaenge.Count)];

                Student neuerStudent = new Student(nachname, vorname, id, studiengang);
                LinkedStudentNode sn = new LinkedStudentNode(neuerStudent);

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

            linkedList.OutputAll();
            Console.WriteLine("------------------------------");
        }

        private static bool mainmenu()
        {
            Console.WriteLine("Programmauswahl: ");
            Console.WriteLine("(0) Programm beenden");
            Console.WriteLine("(1) Studenten hinzufügen");

            if (studentLinkedList == null)
            {
                studentLinkedList = new LinkedList();
            }

            if(studentLinkedList.Count > 0)
            {
                Console.WriteLine("(2) Studenten löschen");
                Console.WriteLine("(3) Studenten suchen");
                Console.WriteLine("(4) Liste sortieren");
                Console.WriteLine("(5) Liste ausgeben");
                Console.WriteLine("(6) Liste löschen");
                Console.WriteLine("(7) Anzahl Elemente ausgeben");
            }

            int operation = Int32.Parse(Console.ReadLine());

            if (operation == 0)
                return false;
            if (operation == 1)
                AddStudentToList();

            if (studentLinkedList.Count > 0)
            { 
                if(operation == 5)
                    studentLinkedList.OutputAll();
            }

            return true;
        }

        private static void AddStudentToList()
        {
            Console.WriteLine("Sie wollen einen Studenten hinzufügen. Ein Student besteht aus Vorname, Nachname, Matrikelnummer und dem Studiengang.");
            Console.WriteLine("Vorname: ");
            string vorname = Console.ReadLine();

            Console.WriteLine("Nachname: ");
            string nachname = Console.ReadLine();

            Console.WriteLine("Matrikelnummer: ");
            string matrikelnummer = Console.ReadLine();

            Console.WriteLine("Studiengang: ");
            string studiengang = Console.ReadLine();

            Student neuerStudent = new Student(nachname, vorname, Int32.Parse(matrikelnummer), studiengang);

            Console.WriteLine("Wollen Sie den Studenten am Anfang oder am Ende hinzufügen?");
            Console.WriteLine("(1) Anfang, (2) Ende");
            int operation = Int32.Parse(Console.ReadLine());

            if (operation == 1)
            {
                if (!studentLinkedList.AddFirst(new LinkedStudentNode(neuerStudent)))
                {
                    Console.WriteLine("Fehler beim Hinzufügen des Studenten.");
                }
            }
            else if (operation == 2)
            {
                if (!studentLinkedList.AddLast(new LinkedStudentNode(neuerStudent)))
                {
                    Console.WriteLine("Fehler beim Hinzufügen des Studenten.");
                }
            }


        }
    }
}
