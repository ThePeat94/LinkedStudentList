using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class DoubleLinkedStudentNode
    {
        /// <summary>
        /// Der zu speicherende Datensatz
        /// </summary>
        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        /// <summary>
        /// Der nachfolgende Knoten
        /// </summary>
        public DoubleLinkedStudentNode Nachfolger
        {
            get { return nachfolger; }
            set { nachfolger = value; }
        }

        /// <summary>
        /// Der vorhergehende Knoten
        /// </summary>
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
