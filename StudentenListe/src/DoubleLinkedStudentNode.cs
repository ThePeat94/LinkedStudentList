using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class DoubleLinkedStudentNode
    {
        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        public DoubleLinkedStudentNode Nachfolger
        {
            get { return nachfolger; }
            set { nachfolger = value; }
        }

        public DoubleLinkedStudentNode Vorgaenger
        {
            get { return vorgaenger; }
            set { vorgaenger = value; }
        }

        private Student student;
        private DoubleLinkedStudentNode vorgaenger;
        private DoubleLinkedStudentNode nachfolger;

        public DoubleLinkedStudentNode(Student student)
        {
            this.student = student;
        }
    }
}
