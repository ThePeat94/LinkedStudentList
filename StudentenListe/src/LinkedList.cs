using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class LinkedList
    {

        public enum SortMode { MATRIKELNUMMER, STUDIENGANG }

        public enum SortOrder { ASCENDING, DESCENDING }

        public enum SearchMode { VORNAME, NACHNAME, VORUNDNACHNAME, MATRIKELNUMMER, STUDIENGANG }

        public int Count
        {
            get { return count; }
            private set { count = value; }
        }
        private LinkedStudentNode head;
        private int count = 0;

        public LinkedList(LinkedStudentNode head)
        {
            this.head = head;
            count++;
        }

        public LinkedList()
        {
            
        }

        public bool AddLast(LinkedStudentNode neuesElement)
        {
            try
            {
                if (head != null)
                {
                    LinkedStudentNode current = head;

                    while (current.Nachfolger != null)
                    {
                        current = current.Nachfolger;
                    }

                    current.Nachfolger = neuesElement;
                    count++;
                    return true;
                }
                head = neuesElement;
                count++;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool AddFirst(LinkedStudentNode toAdd)
        {
            try
            {
                if (head == null)
                {
                    head = toAdd;
                    count++;
                    return true;
                }

                toAdd.Nachfolger = head;
                head = toAdd;
                count++;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public LinkedStudentNode ElementAt(int index)
        {
            if (ValidityCheck(index))
            {
                LinkedStudentNode current = head;
                for (int i = 1; i <= index; i++)
                {
                    current = current.Nachfolger;
                }
                return current;
            }

            return null;
        }

        public void OutputAll()
        {
            if (ValidityCheck())
            {
                LinkedStudentNode current = head;
                Console.WriteLine(current.Student.ToString());

                // Solange ein Nachfolger existiert, gibt es noch einen Studenten zur Darstellung
                while (current.Nachfolger != null)
                {
                    current = current.Nachfolger;
                    Console.WriteLine(current.Student.ToString());
                }
            }
        }

        public bool DeleteAt(int index)
        {
            try
            {
                if (ValidityCheck(index))
                {


                    if (index == 0)
                    {
                        head = head.Nachfolger;
                        count--;
                        return true;
                    }

                    LinkedStudentNode toDelete = ElementAt(index);
                    LinkedStudentNode predecessor = ElementAt(index - 1);

                    predecessor.Nachfolger = toDelete.Nachfolger;
                    count--;
                    return true;

                }
                return false;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool DeleteAll()
        {
            if (ValidityCheck())
            {
                head = null;
                count = 0;
                return true;
            }
            return false;
        }

        public void OutputCount()
        {
            Console.WriteLine(String.Format("Anzahl Elemente in der Liste: {0}", count));
        }

        /// <summary>
        /// Sucht nach Studenten
        /// </summary>
        /// <param name="searchMode">Das Kriterium, nach welchem gesucht werden soll.</param>
        /// <param name="suchparameter">Die Werte, nach denen gefiltert werden sollen. (Soll nach Vor- und Nachname gesucht werden, so steht der Vorname an erster, der Nachname an zweiter Stelle)</param>
        /// <returns></returns>
        public List<Student> SearchForStudent(SearchMode searchMode, params string[] suchparameter)
        {
            if (ValidityCheck())
            {
                if (suchparameter.Length == 0)
                    return null;

                List<Student> result = new List<Student>();
                LinkedStudentNode current = head;

                do
                {
                    Student toAdd = null;
                    if (searchMode == SearchMode.VORNAME && current.Student.Vorname == suchparameter[0])
                    {
                        toAdd = current.Student;
                    }
                    else if (searchMode == SearchMode.NACHNAME && current.Student.Nachname == suchparameter[0])
                    {
                        toAdd = current.Student;
                    }
                    else if (searchMode == SearchMode.VORUNDNACHNAME && current.Student.Vorname == suchparameter[0] && current.Student.Nachname == suchparameter[1])
                    {
                        toAdd = current.Student;
                    }
                    else if (searchMode == SearchMode.MATRIKELNUMMER && current.Student.Matrikelnummer == Int32.Parse(suchparameter[0]))
                    {
                        // Wenn nach der Matrikelnummer gesucht wird, so wird nur ein Eintrag existieren
                        // Diesen der Liste hinzufügen und zurückgeben
                        result.Add(current.Student);
                        return result;
                    }
                    else if (searchMode == SearchMode.STUDIENGANG && current.Student.Studiengang == suchparameter[0])
                    {
                        toAdd = current.Student;
                    }

                    if (toAdd != null)
                    {
                        result.Add(toAdd);
                    }

                    current = current.Nachfolger;
                } while (current != null); // Ist null, wenn es keinen Nachfolger mehr gibt

                return result;
            }

            return null;
        }

        public void SelectionSortList(SortMode sortMode)
        {
            if (ValidityCheck())
            {
                for (int i = 0; i < count; i++)
                {

                    int currMin = i;
                    for (int j = i + 1; j < count; j++)
                    {
                        LinkedStudentNode minElement = ElementAt(currMin);
                        LinkedStudentNode jElement = ElementAt(j);

                        if (sortMode == SortMode.MATRIKELNUMMER)
                        {
                            // Wenn das Element an der aktuellen kleinsten Stelle größer ist als das jElement, so ist jElement das kleinste Element
                            if (minElement.Student.Matrikelnummer > jElement.Student.Matrikelnummer)
                            {
                                currMin = j;
                            }
                        }

                        if (sortMode == SortMode.STUDIENGANG)
                        {
                            // Wenn das Element an der aktuellen kleinsten Stelle größer ist als das jElement, so ist jElement das kleinste Element
                            if (String.Compare(minElement.Student.Studiengang, jElement.Student.Studiengang, StringComparison.OrdinalIgnoreCase) > 0)
                            {
                                currMin = j;
                            }
                        }

                    }
                    Student toSwap = ElementAt(currMin).Student;
                    ElementAt(currMin).Student = ElementAt(i).Student;
                    ElementAt(i).Student = toSwap;
                }
            }
        }

        public void InsertionSort(SortMode sortMode)
        {
            // Erste Element ist bereits sortiert ("Linker Teil")
            for (int i = 1; i < count; i++)
            {
                Student einzusortierendeElement = ElementAt(i).Student;
                int idLinks = i;

                // Von rechts nach links laufen und alle größeren Elemente nach rechts verschieben
                if (sortMode == SortMode.MATRIKELNUMMER)
                {
                    while (idLinks > 0 && einzusortierendeElement.Matrikelnummer < ElementAt(idLinks - 1).Student.Matrikelnummer)
                    {
                        // Das Element links einen nach rechts verschieben
                        ElementAt(idLinks).Student = ElementAt(idLinks - 1).Student;
                        idLinks--;
                    }
                }
                else if (sortMode == SortMode.STUDIENGANG)
                {
                    while (idLinks > 0 && String.Compare(einzusortierendeElement.Studiengang, ElementAt(idLinks - 1).Student.Studiengang, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        // Das Element links einen nach rechts verschieben
                        ElementAt(idLinks).Student = ElementAt(idLinks - 1).Student;
                        idLinks--;
                    }
                }


                // Nach der while-Schleife wissen wir, wo unser aktuelles Element eingeordnet werden muss
                ElementAt(idLinks).Student = einzusortierendeElement;

            }
        }
           

        private bool ValidityCheck()
        {
            if (head == null)
            {
                Console.WriteLine("Es existiert noch kein Element in der Liste.");
                return false;
            }
            return true;
        }

        private bool ValidityCheck(int index)
        {
            if (ValidityCheck())
            {
                if (index < 0)
                {
                    Console.WriteLine("Es wurde ein Index kleiner 0 angegeben. Ungültige Eingabe.");
                    return false;
                }

                if (index > count)
                {
                    Console.WriteLine("Es existieren zu wenig Elemente für diesen Index.");
                    OutputCount();
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
