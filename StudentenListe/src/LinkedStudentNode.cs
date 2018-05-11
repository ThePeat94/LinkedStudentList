using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    /// <summary>
    /// Diese Klasse stellt einen Knoten in einer verketteten Liste dar.
    /// </summary>
    class LinkedStudentNode
    {
        /// <summary>
        /// Der enthaltene Nettodatensatz.
        /// </summary>
        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        /// <summary>
        /// Der darauffolgende Datensatz
        /// </summary>
        public LinkedStudentNode Nachfolger
        {
            get { return nachfolger; }
            set { nachfolger = value; }
        }

        private Student student;
        private LinkedStudentNode nachfolger;


        /// <summary>
        /// Erstellt einen neuen Knoten.
        /// </summary>
        /// <param name="student">Der Datensatz, der beinhaltet wird.</param>
        public LinkedStudentNode(Student student)
        {
            this.student = student;
        }
    }
}
