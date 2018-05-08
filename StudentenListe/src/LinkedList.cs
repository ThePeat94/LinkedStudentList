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
        private StudentNode head;
        private int count = 0;

        public LinkedList(StudentNode head)
        {
            this.head = head;
        }

        public LinkedList()
        {
            
        }

        public bool AddLast(StudentNode neuesElement)
        {
            if (head != null)
            {
                if (head.AddNachfolger(neuesElement))
                {
                    count++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                head = neuesElement;
                count++;
                return true;
            }
        }

        public bool AddFirst(StudentNode toAdd)
        {
            try
            {
                if (head == null)
                {
                    head = toAdd;
                    count++;
                    return true;
                }


                if (head.Student.Matrikelnummer == toAdd.Student.Matrikelnummer)
                {
                    return false;
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

        public StudentNode ElementAt(int index)
        {
            if (ValidityCheck(index))
            {
                StudentNode current = head;
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
                StudentNode current;
                current = head;
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

                    StudentNode toDelete = ElementAt(index);
                    StudentNode predecessor = ElementAt(index - 1);

                    predecessor.Nachfolger = toDelete.Nachfolger;
                    count--;
                    return true;

                }
                else
                {
                    return false;
                }
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
            else
            {
                return false;
            }
        }

        public void OutputCount()
        {
            Console.WriteLine(String.Format("Anzahl Elemente in der Liste: {0}", count));
        }

        public Student SearchForStudent(int matrikelnummer, string studiengang)
        {
            if (ValidityCheck())
            {
                StudentNode current = head;

                do
                {
                    if (current.Student.Matrikelnummer == matrikelnummer && current.Student.Studiengang == studiengang)
                    {
                        return current.Student;
                    }
                    current = current.Nachfolger;
                } while (current != null); // Ist null, wenn es keinen Nachfolger mehr gibt
            }

            return null;
        }

        public List<Student> SearchForStudent(string vorname, string nachname)
        {
            if (ValidityCheck())
            {
                List<Student> result = new List<Student>();
                StudentNode current = head;

                do
                {
                    if (current.Student.Vorname == vorname && current.Student.Nachname == nachname)
                    {
                        result.Add(current.Student);
                    }
                    current = current.Nachfolger;
                } while (current != null); // Ist null, wenn es keinen Nachfolger mehr gibt

                return result;
            }

            return null;
        }

        public void SelectionSortList()
        {
            if (ValidityCheck())
            {
                StudentNode current = head;
                do
                {
                    StudentNode next = current.Nachfolger;
                    while(next != null)
                    {
                        if (current.Student.Matrikelnummer > next.Student.Matrikelnummer)
                        {
                            Student toSwap = next.Student;
                            next.Student = current.Student;
                            current.Student = toSwap;
                            break;
                        }
                        next = next.Nachfolger;
                    }

                    current = current.Nachfolger;
                } while (current != null);
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
