using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class StudentUtils
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
        /// Sucht nach Studenten anhand eines Suchkriteriums und Suchwerten
        /// </summary>
        /// <param name="searchMode">Das Kriterium, nach welchem gesucht werden soll.</param>
        /// <param name="suchparameter">Die Werte, nach denen gefiltert werden sollen. (Soll nach Vor- und Nachname gesucht werden, so steht der Vorname an erster, der Nachname an zweiter Stelle)</param>
        /// <returns>Die Liste der Studenten, die auf das Suchkriterium und die Suchwerte passen</returns>
        public static List<Student> SearchForStudent(SingleLinkedList<Student> liste, SearchMode searchMode, params string[] suchparameter)
        {
            if (liste.Count >= 0)
            {
                // Wenn keine Parameter angegeben wurden, braucht man nicht suchen
                if (suchparameter.Length == 0)
                    return null;

                List<Student> result = new List<Student>();
                SingleLinkedNode<Student> current = liste.ElementAt(0);

                do
                {
                    Student toAdd = null;

                    // Nach Suchmodus filtern und ggf. den aktuellen Studenten prüfen, ob dieser passt
                    if (searchMode == SearchMode.VORNAME && current.Data.Vorname == suchparameter[0])
                    {
                        toAdd = current.Data;
                    }
                    else if (searchMode == SearchMode.NACHNAME && current.Data.Nachname == suchparameter[0])
                    {
                        toAdd = current.Data;
                    }
                    else if (searchMode == SearchMode.VORUNDNACHNAME && current.Data.Vorname == suchparameter[0] && current.Data.Nachname == suchparameter[1])
                    {
                        toAdd = current.Data;
                    }
                    else if (searchMode == SearchMode.MATRIKELNUMMER && current.Data.Matrikelnummer == Int32.Parse(suchparameter[0]))
                    {
                        toAdd = current.Data;
                    }
                    else if (searchMode == SearchMode.STUDIENGANG && current.Data.Studiengang == suchparameter[0])
                    {
                        toAdd = current.Data;
                    }

                    // Nur wenn ein Data gefunden wurde, muss dieser hinzugefügt werden
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
        public static void SelectionSortList(SingleLinkedList<Student> liste, SortMode sortMode)
        {
                // Wenn die Liste nur ein Element beinhaltet, muss nicht sortiert werden
                if (liste.Count <= 1)
                    return;

                SingleLinkedNode<Student> aktuellesElement = liste.ElementAt(0);
                for (int i = 0; i < liste.Count; i++)
                {
                    // Da immer das nachfolgende Element betrachtet wird, holen wir uns den Nachfolger
                    // Erst den Nachfolger holen, wenn es zuvor einen Durchgang gab
                    if (i > 0)
                        aktuellesElement = aktuellesElement.Nachfolger;

                    SingleLinkedNode<Student> aktuellKleinstesElement = aktuellesElement;

                    SingleLinkedNode<Student> vergleichendesElement = aktuellesElement.Nachfolger;

                    for (int j = i + 1; j < liste.Count; j++)
                    {
                        if (sortMode == SortMode.MATRIKELNUMMER)
                        {
                            // Wenn das Element an der aktuellen kleinsten Stelle größer ist als das jElement, so ist jElement das kleinste Element
                            if (aktuellKleinstesElement.Data.Matrikelnummer > vergleichendesElement.Data.Matrikelnummer)
                            {
                                aktuellKleinstesElement = vergleichendesElement;
                            }
                        }
                        else if (sortMode == SortMode.STUDIENGANG)
                        {
                            // minElements Studiengang ist alphabetisch nach jElements Studiengang
                            if (String.Compare(aktuellKleinstesElement.Data.Studiengang, vergleichendesElement.Data.Studiengang, StringComparison.OrdinalIgnoreCase) > 0)
                            {
                                aktuellKleinstesElement = vergleichendesElement;
                            }
                        }
                        // Anschließend das nächste Element
                        vergleichendesElement = vergleichendesElement.Nachfolger;
                    }
                    // Tauschen
                    Student toSwap = aktuellKleinstesElement.Data;
                    aktuellKleinstesElement.Data = aktuellesElement.Data;
                    aktuellesElement.Data = toSwap;
                }
            
        }

        /// <summary>
        /// Sortiert die Liste aufsteigend nach dem Insertionsortverfahren
        /// </summary>
        /// <param name="sortMode">Der Sortiermodus bestimmt ob nach Matrikelnummer oder Studiengang sortiert werden soll</param>
        public static void InsertionSort(SingleLinkedList<Student> liste, SortMode sortMode)
        {
            // Wenn die Liste nur ein Element beinhaltet, muss nicht sortiert werden
            if (liste.Count <= 1)
                return;

            SingleLinkedNode<Student> zuSortierenderKnoten = liste.ElementAt(0);

            // Erste Element ist bereits sortiert ("Linker Teil")
            for (int i = 1; i < liste.Count; i++) // n - 1
            {
                zuSortierenderKnoten = zuSortierenderKnoten.Nachfolger;
                Student einzusortierendeElement = zuSortierenderKnoten.Data;

                int idLinks = i;

                if (sortMode == SortMode.MATRIKELNUMMER)
                {
                    // Von rechts nach links laufen und alle größeren Elemente im "linken Teil" nach rechts verschieben
                    while (idLinks > 0 && einzusortierendeElement.Matrikelnummer < liste.ElementAt(idLinks - 1).Data.Matrikelnummer)
                    {
                        // Das Element links einen nach rechts verschieben
                        liste.ElementAt(idLinks).Data = liste.ElementAt(idLinks - 1).Data;
                        idLinks--;
                    }
                }
                else if (sortMode == SortMode.STUDIENGANG)
                {
                    // Von rechts nach links laufen und alle größeren Elemente im "linken Teil" nach rechts verschieben
                    while (idLinks > 0 && String.Compare(einzusortierendeElement.Studiengang, liste.ElementAt(idLinks - 1).Data.Studiengang, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        // Das Element links einen nach rechts verschieben
                        liste.ElementAt(idLinks).Data = liste.ElementAt(idLinks - 1).Data;
                        idLinks--;
                    }
                }


                // Nach der while-Schleife wissen wir, wo unser aktuelles Element eingeordnet werden muss
                liste.ElementAt(idLinks).Data = einzusortierendeElement;

            }
            
        }
    }
}
