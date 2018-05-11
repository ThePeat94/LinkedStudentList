using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class DoubleLinkedList
    {
        /// <summary>
        /// Anzahl Elemente in der Liste
        /// </summary>
        public int Count
        {
            get { return count; }
            private set { count = value; }
        }

        /// <summary>
        /// Der Kopf der Liste
        /// </summary>
        private DoubleLinkedStudentNode head;
        private int count = 0;

        public DoubleLinkedList(DoubleLinkedStudentNode head)
        {
            this.head = head;
            count++;
        }

        public DoubleLinkedList()
        {

        }

        /// <summary>
        /// Fügt einen Knoten am Ende der Liste hinzu
        /// </summary>
        /// <param name="neuerKnoten">Der Knoten, der hinzugefügt werden soll</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool AddLast(DoubleLinkedStudentNode neuerKnoten)
        {
            try
            {

                if (head != null)
                {
                    DoubleLinkedStudentNode current = head;

                    // letzte Element herausfinden
                    while (current.Nachfolger != null)
                    {
                        current = current.Nachfolger;
                    }
                    
                    // Der neue Knoten ist der Nachfolger des letzten...
                    current.Nachfolger = neuerKnoten;

                    // ... und der letzte Knoten ist der Vorgänger des neuen Knotens
                    neuerKnoten.Vorgaenger = current;
                    count++;
                    return true;
                }

                // Wenn es keinen Kopf gibt, so ist der neue Knoten der Kopf
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
        /// Fügt einen Knoten am Anfang der Liste hinzu
        /// </summary>
        /// <param name="neuerKnoten">Der neue Knoten am Anfang der Liste</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool AddFirst(DoubleLinkedStudentNode neuerKnoten)
        {
            try
            {
                // Ist der Kopf null, so ist der neue Knoten der Kopf
                if (head == null)
                {
                    head = neuerKnoten;
                    count++;
                    return true;
                }

                // Tauschen und Vorgänger und Nachfolger entsprechend setzen
                DoubleLinkedStudentNode temp = head;
                head = neuerKnoten;
                neuerKnoten.Nachfolger = temp;
                temp.Vorgaenger = neuerKnoten;

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
        /// Ruft ein Knoten am gegebenen Index ab
        /// </summary>
        /// <param name="index">Der Index des Knoten</param>
        /// <returns>Den Knoten am gegebenen Index</returns>
        public DoubleLinkedStudentNode ElementAt(int index)
        {
            if (ValidityCheck(index))
            {
                DoubleLinkedStudentNode current = head;
                for (int i = 1; i <= index; i++)
                {
                    current = current.Nachfolger;
                }
                return current;
            }

            return null;
        }

        /// <summary>
        /// Löscht einen Knoten am gegebenen Index
        /// </summary>
        /// <param name="index">Der Index des zu löschenden Knoten</param>
        /// <returns>true bei Erfolg, sonst false</returns>
        public bool DeleteAt(int index)
        {
            try
            {
                if (ValidityCheck(index))
                {

                    // Kopf löschen
                    if (index == 0)
                    {
                        head = head.Nachfolger;
                        count--;
                        return true;
                    }
                    
                    DoubleLinkedStudentNode toDelete = ElementAt(index);

                    // Hier erspart man sich die Suche nach dem Vorgänger
                    DoubleLinkedStudentNode predecessor = toDelete.Vorgaenger;

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
        /// Prüft, ob mit der Liste gearbeitet werden kann
        /// </summary>
        /// <returns>true, wenn mit der Liste gearbeitet werden kann, sonst false</returns>
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
        /// Prüft, ob mit dem gegebenen Index gearbeitet werden kann
        /// </summary>
        /// <param name="index">Der zu prüfende Index</param>
        /// <returns>true, wenn mit dem Index gearbeitet werden kann, sonst false</returns>
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
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gibt die Liste auf der Konsole aus
        /// </summary>
        public void OutputAll()
        {
            if (ValidityCheck())
            {
                DoubleLinkedStudentNode current = head;
                Console.WriteLine(current.Student.ToString());

                // Solange ein Nachfolger existiert, gibt es noch einen Studenten zur Darstellung
                while (current.Nachfolger != null)
                {
                    current = current.Nachfolger;
                    Console.WriteLine(current.Student.ToString());
                }
            }
        }
    }
}
