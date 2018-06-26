using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StudentenListe.src;

namespace StudentenListe
{
    class Program
    {
        #region Listen für zufällige Studenten
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
        #endregion

        private static SingleLinkedList<Student> studentLinkedList;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Sie haben das Studentenlistenprogramm gestartet.");
                bool continueProgram = true;
                while (continueProgram)
                {
                    continueProgram = mainmenu();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            
        }

        /// <summary>
        /// Generiert eine zufällige Anzahl an Studenten, welche der Liste hinzugefügt werden
        /// </summary>
        private static void GenerateRandomList()
        {
            Console.WriteLine(String.Format("Zur zufälligen Generierung von Studenten stehen derzeit {0} Vornamen, {1} Nachnamen und {2} Studiengänge zur Auswahl.", vornamen.Count, nachnamen.Count, studiengaenge.Count));
            Console.WriteLine("Wieviele zufällige Studenten sollen generiert und hinzugefügt werden?");
            int anzahl = InputNumber(0, Int32.MaxValue);
            Random rnd = new Random();
            //int id = rnd.Next(73628, 78329);
            int id = 0;
            for (int i = 0; i < anzahl; i++)
            {
                id = rnd.Next(73628, 78329);
                string vorname = vornamen[rnd.Next(0, vornamen.Count)];
                string nachname = nachnamen[rnd.Next(0, nachnamen.Count)];
                string studiengang = studiengaenge[rnd.Next(0, studiengaenge.Count)];

                Student neuerStudent = new Student(nachname, vorname, id, studiengang);
                SingleLinkedNode<Student> sn = new SingleLinkedNode<Student>(neuerStudent);

                if (!studentLinkedList.AddLast(sn))
                {
                    Console.WriteLine("Element hinzufügen fehlgeschlagen!");
                    Console.WriteLine(sn.Data.ToString());
                }


                //id++;
            }

            Console.WriteLine("Liste nach der zufälligem Generierung von Studenten: ");
            studentLinkedList.OutputAll(true);
        }

        /// <summary>
        /// Das Hauptmenü
        /// </summary>
        /// <returns>true, wenn das Programm fortgesetzt werden soll, false wenn nicht</returns>
        private static bool mainmenu()
        {
            Console.WriteLine("------Programmauswahl------");
            Console.WriteLine("(0) Programm beenden");
            Console.WriteLine("(1) Studenten hinzufügen");
            Console.WriteLine("(2) Zufällige Studenten generieren und hinzufügen (für Testzwecke geeignet)");
            int maxZahl = 2;

            if (studentLinkedList == null)
            {
                studentLinkedList = new SingleLinkedList<Student>();
            }

            if(studentLinkedList.Count > 0)
            {
                Console.WriteLine("(3) Studenten löschen");
                Console.WriteLine("(4) Studenten suchen");
                Console.WriteLine("(5) Liste sortieren");
                Console.WriteLine("(6) Liste ausgeben");
                Console.WriteLine("(7) Liste löschen");
                Console.WriteLine("(8) Anzahl Elemente ausgeben");
                Console.WriteLine("(9) Element einer gewissen Stelle ausgeben");
                maxZahl = 9;
            }

            int operation = InputNumber(0, maxZahl);

            if (operation == 0)
                return false;

            if (operation == 1)
                AddStudentToList();

            if(operation == 2)
                GenerateRandomList();

            if (studentLinkedList.Count > 0)
            {
                if (operation == 3)
                    DeleteStudent();

                if (operation == 4)
                    SearchStudent();

                if (operation == 5)
                    SortList();

                if(operation == 6)
                    studentLinkedList.OutputAll(true);

                if (operation == 7)
                    DeleteList();

                if (operation == 8)
                    studentLinkedList.OutputCount();

                if (operation == 9)
                    GetStudentAt();
            }

            return true;
        }

        /// <summary>
        /// Menü zur Auswahl eines Studenten anhand eines Index
        /// </summary>
        private static void GetStudentAt()
        {
            Console.WriteLine("Den wievielten Studenten der Liste wollen Sie ausgeben?");
            // minus 1, weil 0 basiert
            int index = InputNumber(1, studentLinkedList.Count) - 1;
            SingleLinkedNode<Student> element = studentLinkedList.ElementAt(index);

            if (element != null)
            {
                Console.WriteLine("Folgender Data wurde gefunden: ");
                Console.WriteLine(element.Data);
            }
            else
            {
                Console.WriteLine("Es konnte kein Data gefunden werden.");
            }
        }

        /// <summary>
        /// Menü zum Löschen der Liste
        /// </summary>
        private static void DeleteList()
        {
            Console.WriteLine("Wollen Sie die Liste wirklich löschen? (Y/N)");
            string yesno = Console.ReadLine();

            if (yesno.ToLower() == "y")
            {
                if (studentLinkedList.DeleteAll())
                {
                    Console.WriteLine("Liste erfolgreich gelöscht.");
                }
                else
                {
                    Console.WriteLine("Fehler beim Löschen der Liste.");
                }
            }
        }

        /// <summary>
        /// Menü zum Sortieren der Liste
        /// </summary>
        private static void SortList()
        {
            Console.WriteLine("Sie können die Studenten nach folgenden Kriterien sortieren:");
            Console.WriteLine("(1) Matrikelnummer");
            Console.WriteLine("(2) Studiengang");

            Console.WriteLine("Wonach wollen Sie sortieren?");
            int sortMode = InputNumber(1, 2);

            // minus 1, weil 0 basiert
            StudentUtils.SortMode sm = (StudentUtils.SortMode) sortMode - 1;
            StudentUtils.SelectionSortList(studentLinkedList, sm);
            studentLinkedList.OutputAll(false);
        }

        /// <summary>
        /// Menü zur Suche nach einem Studenten
        /// </summary>
        private static void SearchStudent()
        {
            Console.WriteLine("Sie können nach folgenden Kriterien suchen: ");
            Console.WriteLine("(1) Vorname ");
            Console.WriteLine("(2) Nachname ");
            Console.WriteLine("(3) Vor- und Nachname");
            Console.WriteLine("(4) Matrikelnummer");
            Console.WriteLine("(5) Studiengang");
            Console.WriteLine("Wonach wollen Sie suchen?");
            int operation = InputNumber(1, 5);
            int anzParameter = (operation == 3 ? 2 : 1);
            string[] suchwerte = new string[anzParameter];

            if (anzParameter == 1)
            {
                Console.WriteLine("Geben Sie den Suchwert ein: ");
            }
            else
            {
                Console.WriteLine("Geben Sie die Suchwerte ein: ");
            }

            for (int i = 0; i < anzParameter; i++)
            {
                suchwerte[i] = Console.ReadLine();
            }

            // minus 1, weil 0 basiert
            StudentUtils.SearchMode sm = (StudentUtils.SearchMode) operation - 1;
            List<Student> studentsFound = StudentUtils.SearchForStudent(studentLinkedList, sm, suchwerte);

            if (studentsFound.Count > 0)
            {
                Console.WriteLine("Folgende Studenten wurden gefunden: ");
                studentsFound.ForEach(x => Console.WriteLine(x));
            }
            else
            {
                Console.WriteLine("Es wurde kein Data gefunden, der den Suchkriterien und -werten entspricht.");
            }
        }

        /// <summary>
        /// Menü zum Löschen eines Studenten anhand seines Index in der Liste
        /// </summary>
        private static void DeleteStudent()
        {
            studentLinkedList.OutputAll(true);
            Console.WriteLine("Geben Sie die Indexzahl des zu löschenden Studenten an: ");
            int index = InputNumber(1, studentLinkedList.Count) - 1;
            if (!studentLinkedList.DeleteAt(index))
            {
                Console.WriteLine("Fehler beim Löschen des Studenten.");
            }
        }

        /// <summary>
        /// Menü zum Hinzufügen eines Studenten
        /// </summary>
        private static void AddStudentToList()
        {
            Console.WriteLine("Sie wollen einen Studenten hinzufügen. Ein Data besteht aus Vorname, Nachname, Matrikelnummer und dem Studiengang.");
            Console.WriteLine("Vorname: ");
            string vorname = Console.ReadLine();

            Console.WriteLine("Nachname: ");
            string nachname = Console.ReadLine();

            Console.WriteLine("Matrikelnummer: ");
            int matrikelnummer = InputNumber(0, Int32.MaxValue);


            Console.WriteLine("Studiengang: ");
            string studiengang = Console.ReadLine();

            Student neuerStudent = new Student(nachname, vorname, matrikelnummer, studiengang);

            Console.WriteLine("Wollen Sie den Studenten am Anfang oder am Ende hinzufügen?");
            Console.WriteLine("(1) Anfang, (2) Ende");
            int operation = InputNumber(1, 2);

            if (operation == 1)
            {
                if (!studentLinkedList.AddFirst(new SingleLinkedNode<Student>(neuerStudent)))
                {
                    Console.WriteLine("Fehler beim Hinzufügen des Studenten.");
                }
            }
            else if (operation == 2)
            {
                if (!studentLinkedList.AddLast(new SingleLinkedNode<Student>(neuerStudent)))
                {
                    Console.WriteLine("Fehler beim Hinzufügen des Studenten.");
                }
            }

            studentLinkedList.OutputAll(false);
        }

        /// <summary>
        /// Versucht, solange eine Eingabe einzulesen, bis eine Zahl korrekt eingegeben wurde, welche zwischen einem Minimum und Maximum liegt
        /// </summary>
        /// <param name="min">Das Minimum</param>
        /// <param name="max">Das Maximum</param>
        /// <returns>Die eingelesene Zahl</returns>
        private static int InputNumber(int min, int max)
        {
            int zahl;
            while (true)
            {
                string eingabe = Console.ReadLine();

                if (Int32.TryParse(eingabe, out zahl))
                {
                    if (zahl >= min && zahl <= max)
                    {
                        break;
                    }
                }
                Console.WriteLine("Es wurde eine ungültige Zahl eingegeben. Versuchen Sie es erneut.");
            }
            return zahl;
        }
    }
}
