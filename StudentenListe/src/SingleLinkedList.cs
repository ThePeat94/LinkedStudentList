using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class SingleLinkedList<T>
    {
        /// <summary>
        /// Die Anzahl Datensätze, die die Liste beinhaltet
        /// </summary>
        public int Count
        {
            get { return count; }
            private set { count = value; }
        }
        private SingleLinkedNode<T> head;
        private int count = 0;

        public SingleLinkedList(SingleLinkedNode<T> head)
        {
            this.head = head;
            count++;
        }

        public SingleLinkedList()
        {
            
        }

        /// <summary>
        /// Fügt einen Knoten am Ende der Liste hinzu
        /// </summary>
        /// <param name="neuerKnoten">Der Knoten, der am Ende hinzugefügt werden soll.</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool AddLast(SingleLinkedNode<T> neuerKnoten)
        {
            try
            {
                // Wenn es keinen Kopf gibt, muss der letzte Datensatz erst ermittelt werden
                if (head != null)
                {
                    SingleLinkedNode<T> current = head;

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
        public bool AddFirst(SingleLinkedNode<T> neuerKnoten)
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
        public SingleLinkedNode<T> ElementAt(int index)
        {
            if (ValidityCheck(index))
            {
                SingleLinkedNode<T> current = head;
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
                SingleLinkedNode<T> current = head;
                int index = 1;
                // Solange ein Nachfolger existiert, gibt es noch einen Studenten zur Darstellung
                while (current != null)
                {
                    if(showIndex)
                        Console.WriteLine(String.Format("[{0}]: {1}", index, current.Data));
                    else
                        Console.WriteLine(String.Format("{0}", current.Data));
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
                    SingleLinkedNode<T> toDelete = ElementAt(index);
                    SingleLinkedNode<T> predecessor = ElementAt(index - 1);

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
