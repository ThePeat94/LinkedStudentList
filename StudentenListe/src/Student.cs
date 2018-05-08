using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentenListe.src
{
    class Student
    {
        public string Nachname
        {
            get { return nachname; }
        }

        public string Vorname
        {
            get { return vorname; }
        }

        public int Matrikelnummer
        {
            get { return matrikelnummer; }
        }

        public string Studiengang
        {
            get { return studiengang; }
        }

        private string nachname;
        private string vorname;
        private int matrikelnummer;
        private string studiengang;

        public Student(string nachname, string vorname, int matrikelnummer, string studiengang)
        {
            this.nachname = nachname;
            this.vorname = vorname;
            this.matrikelnummer = matrikelnummer;
            this.studiengang = studiengang;
        }




        public override string ToString()
        {
            return String.Format("{0} - {1} {2} studiert {3}", matrikelnummer, vorname, nachname, studiengang);
        }
    }
}
