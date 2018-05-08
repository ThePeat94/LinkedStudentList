using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class StudentNode
    {
        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        public StudentNode Nachfolger
        {
            get { return nachfolger; }
            set { nachfolger = value; }
        }

        private Student student;
        private StudentNode nachfolger;

        public StudentNode(Student student)
        {
            this.student = student;
        }

        public bool AddNachfolger(StudentNode nachfolger)
        {
            try
            {
                if (this.nachfolger == null)
                {
                    this.nachfolger = nachfolger;
                    return true;
                }
               
                // Student bereits vorhanden => muss nicht hinzugefügt werden
                if (student.Matrikelnummer == nachfolger.Student.Matrikelnummer)
                {
                    return false;
                }
                
                // Solange es einen Nachfolger gibt, muss dieser hinten angefügt werden
                return this.nachfolger.AddNachfolger(nachfolger);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
