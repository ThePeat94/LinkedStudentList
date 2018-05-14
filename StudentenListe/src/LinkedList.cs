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
        /// <summary>
        /// Enumeration für die Sortierverfahren
        /// </summary>
        public enum SortMode { MATRIKELNUMMER, STUDIENGANG }

        /// <summary>
        /// Enumeration für das Suchverfahren
        /// </summary>
        public enum SearchMode { VORNAME, NACHNAME, VORUNDNACHNAME, MATRIKELNUMMER, STUDIENGANG }

        /// <summary>
        /// Die Anzahl Datensätze, die die Liste beinhaltet
        /// </summary>
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

        /// <summary>
        /// Fügt einen Knoten am Ende der Liste hinzu
        /// </summary>
        /// <param name="neuerKnoten">Der Knoten, der am Ende hinzugefügt werden soll.</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool AddLast(LinkedStudentNode neuerKnoten)
        {
            try
            {
                // Wenn es keinen Kopf gibt, muss der letzte Datensatz erst ermittelt werden
                if (head != null)
                {
                    LinkedStudentNode current = head;

                    // Der letzte Datensatz hat keinen Nachfolger -> markiert somit das Ende
                    while (current.Nachfolger != null)
                    {
                        current = current.Nachfolger;
                    }

                    // Der neue Knoten ist der Nachfolger des letzten Datensatzes
                    current.Nachfolger = neuerKnoten;
                    count++;
                    return true;
                }

                // Ansonsten ist der neue Knoten der Kopf
                head = neuerKnoten;
                count++;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Fügt einen neuen Knoten am Anfang der Liste hinzu (Kopf wird neu gesetzt)
        /// </summary>
        /// <param name="neuerKnoten">Der hinzuzufügende Knoten</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool AddFirst(LinkedStudentNode neuerKnoten)
        {
            try
            {

                // Wenn es keinen Kopf gibt, ist der neue Knoten der Kopf
                if (head == null)
                {
                    head = neuerKnoten;
                    count++;
                    return true;
                }

                // Gibt es einen Kopf, so ist dieser der Nachfolger des neuen Knotens
                neuerKnoten.Nachfolger = head;

                // Und der neue Knoten der Kopf
                head = neuerKnoten;
                count++;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        /// <summary>
        /// Gibt den Knoten am gegebenen Index zurück
        /// </summary>
        /// <param name="index">Der Index, an welchen sich das Element befindet (0 basiert)</param>
        /// <returns>Den Knoten am gegebenen Index</returns>
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

        /// <summary>
        /// Gibt alle Datensätze der Liste auf der Konsole aus
        /// </summary>
        /// <param name="showIndex">true, wenn der Index mit dargestellt werden soll, sonst false</param>
        public void OutputAll(bool showIndex)
        {
            if (ValidityCheck())
            {
                LinkedStudentNode current = head;
                int index = 1;
                // Solange ein Nachfolger existiert, gibt es noch einen Studenten zur Darstellung
                while (current != null)
                {
                    if(showIndex)
                        Console.WriteLine(String.Format("[{0}]: {1}", index, current.Student));
                    else
                        Console.WriteLine(String.Format("{0}", current.Student));
                    current = current.Nachfolger;
                    index++;
                }
            }
        }

        /// <summary>
        /// Löscht einen Knoten am gegebenen Index
        /// </summary>
        /// <param name="index">Der Index des zu löschenden Knotens</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool DeleteAt(int index)
        {
            try
            {
                if (ValidityCheck(index))
                {
                    // Wenn der Index 0 ist, so soll der Kopf gelöscht werden
                    if (index == 0)
                    {
                        // Löschen erfolgt, indem es keine Referenz mehr auf den aktuellen Kopf gibt
                        head = head.Nachfolger;
                        count--;
                        return true;
                    }

                    // Zum Löschen werden der zu löschende Knoten und sein Vorgänger benötigt
                    LinkedStudentNode toDelete = ElementAt(index);
                    LinkedStudentNode predecessor = ElementAt(index - 1);

                    // Der Vorgänger erhält als Nachfolger den Nachfolger des zu löschenden Knotens
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

        /// <summary>
        /// Löscht die komplette Liste
        /// </summary>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool DeleteAll()
        {
            if (ValidityCheck())
            {
                // Komplette Liste kann gelöscht werden, indem der Kopf entfernt wird
                head = null;
                count = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stellt die Anzahl der Knoten der Liste dar
        /// </summary>
        public void OutputCount()
        {
            Console.WriteLine(String.Format("Anzahl Elemente in der Liste: {0}", count));
        }

        /// <summary>
        /// Sucht nach Studenten anhand eines Suchkriteriums und Suchwerten
        /// </summary>
        /// <param name="searchMode">Das Kriterium, nach welchem gesucht werden soll.</param>
        /// <param name="suchparameter">Die Werte, nach denen gefiltert werden sollen. (Soll nach Vor- und Nachname gesucht werden, so steht der Vorname an erster, der Nachname an zweiter Stelle)</param>
        /// <returns>Die Liste der Studenten, die auf das Suchkriterium und die Suchwerte passen</returns>
        public List<Student> SearchForStudent(SearchMode searchMode, params string[] suchparameter)
        {
            if (ValidityCheck())
            {
                // Wenn keine Parameter angegeben wurden, braucht man nicht suchen
                if (suchparameter.Length == 0)
                    return null;

                List<Student> result = new List<Student>();
                LinkedStudentNode current = head;

                do
                {
                    Student toAdd = null;

                    // Nach Suchmodus filtern und ggf. den aktuellen Studenten prüfen, ob dieser passt
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
                        toAdd = current.Student;
                    }
                    else if (searchMode == SearchMode.STUDIENGANG && current.Student.Studiengang == suchparameter[0])
                    {
                        toAdd = current.Student;
                    }

                    // Nur wenn ein Student gefunden wurde, muss dieser hinzugefügt werden
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

        /// <summary>
        /// Sortiert die Liste aufsteigend mithilfe des Selectionsorts
        /// </summary>
        /// <param name="sortMode">Der Sortiermodus bestimmt ob nach Matrikelnummer oder Studiengang sortiert werden soll</param>
        public void SelectionSortList(SortMode sortMode)
        {
            if (ValidityCheck())
            {
                // Wenn die Liste nur ein Element beinhaltet, muss nicht sortiert werden
                if (count == 1)
                    return;

                LinkedStudentNode aktuellesElement = head;
                for (int i = 0; i < count; i++)
                {
                    // Da immer das nachfolgende Element betrachtet wird, holen wir uns den Nachfolger
                    // Erst den Nachfolger holen, wenn es zuvor einen Durchgang gab
                    if(i > 0)
                        aktuellesElement = aktuellesElement.Nachfolger;

                    LinkedStudentNode aktuellKleinstesElement = aktuellesElement;

                    LinkedStudentNode vergleichendesElement = aktuellesElement.Nachfolger;

                    for (int j = i + 1; j < count; j++)
                    {
                        if (sortMode == SortMode.MATRIKELNUMMER)
                        {
                            // Wenn das Element an der aktuellen kleinsten Stelle größer ist als das jElement, so ist jElement das kleinste Element
                            if (aktuellKleinstesElement.Student.Matrikelnummer > vergleichendesElement.Student.Matrikelnummer)
                            {
                                aktuellKleinstesElement = vergleichendesElement;
                            }
                        }
                        else if (sortMode == SortMode.STUDIENGANG)
                        {
                            // minElements Studiengang ist alphabetisch nach jElements Studiengang
                            if (String.Compare(aktuellKleinstesElement.Student.Studiengang, vergleichendesElement.Student.Studiengang, StringComparison.OrdinalIgnoreCase) > 0)
                            {
                                aktuellKleinstesElement = vergleichendesElement;
                            }
                        }
                        // Anschließend das nächste Element
                        vergleichendesElement = vergleichendesElement.Nachfolger;
                    }
                    // Tauschen
                    Student toSwap = aktuellKleinstesElement.Student;
                    aktuellKleinstesElement.Student = aktuellesElement.Student;
                    aktuellesElement.Student = toSwap;
                }
            }
        }

        /// <summary>
        /// Sortiert die Liste aufsteigend nach dem Insertionsortverfahren
        /// </summary>
        /// <param name="sortMode">Der Sortiermodus bestimmt ob nach Matrikelnummer oder Studiengang sortiert werden soll</param>
        public void InsertionSort(SortMode sortMode)
        {
            // Wenn die Liste nur ein Element beinhaltet, muss nicht sortiert werden
            if (count == 1)
                return;

            if (ValidityCheck())
            {
                LinkedStudentNode zuSortierenderKnoten = head;

                // Erste Element ist bereits sortiert ("Linker Teil")
                for (int i = 1; i < count; i++) // n - 1
                {
                    zuSortierenderKnoten = zuSortierenderKnoten.Nachfolger;
                    Student einzusortierendeElement = zuSortierenderKnoten.Student;

                    int idLinks = i;

                    if (sortMode == SortMode.MATRIKELNUMMER)
                    {
                        // Von rechts nach links laufen und alle größeren Elemente im "linken Teil" nach rechts verschieben
                        while (idLinks > 0 && einzusortierendeElement.Matrikelnummer < ElementAt(idLinks - 1).Student.Matrikelnummer)
                        {
                            // Das Element links einen nach rechts verschieben
                            ElementAt(idLinks).Student = ElementAt(idLinks - 1).Student;
                            idLinks--;
                        }
                    }
                    else if (sortMode == SortMode.STUDIENGANG)
                    {
                        // Von rechts nach links laufen und alle größeren Elemente im "linken Teil" nach rechts verschieben
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
        }
           

        /// <summary>
        /// Überprüft, ob mit der Liste gearbeitet werden kann
        /// </summary>
        /// <returns>true, wenn es einen Kopf gibt, sonst false</returns>
        private bool ValidityCheck()
        {
            if (head == null)
            {
                Console.WriteLine("Es existiert noch kein Element in der Liste.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Überprüft, ob mit der Liste anhand eines gegebenen Indexes gearbeitet werden kann
        /// </summary>
        /// <param name="index">Der zu prüfende Index</param>
        /// <returns>true, wenn der Index gültig ist, sonst false</returns>
        private bool ValidityCheck(int index)
        {
            if (ValidityCheck())
            {
                if (index < 0)
                {
                    Console.WriteLine("Es wurde ein Index kleiner 0 angegeben. Ungültige Eingabe.");
                    return false;
                }

                if (index >= count)
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
