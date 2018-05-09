using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class DoubleLinkedList
    {
        public int Count
        {
            get { return count; }
            private set { count = value; }
        }
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

        public bool AddLast(DoubleLinkedStudentNode neuesElement)
        {
            try
            {
                if (head != null)
                {
                    DoubleLinkedStudentNode current = head;

                    while (current.Nachfolger != null)
                    {
                        current = current.Nachfolger;
                    }

                    current.Nachfolger = neuesElement;
                    neuesElement.Vorgaenger = current;
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

        public bool AddFirst(DoubleLinkedStudentNode neuerKopf)
        {
            try
            {
                if (head == null)
                {
                    head = neuerKopf;
                    count++;
                    return true;
                }

                DoubleLinkedStudentNode temp = head;
                head = neuerKopf;
                neuerKopf.Nachfolger = temp;
                temp.Vorgaenger = neuerKopf;

                count++;
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

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
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

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
