using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class LinkedStudentNode
    {
        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        public LinkedStudentNode Nachfolger
        {
            get { return nachfolger; }
            set { nachfolger = value; }
        }

        private Student student;
        private LinkedStudentNode nachfolger;

        public LinkedStudentNode(Student student)
        {
            this.student = student;
        }
    }
}
