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
    class SingleLinkedNode<T>
    {
        /// <summary>
        /// Der enthaltene Nettodatensatz.
        /// </summary>
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Der darauffolgende Datensatz
        /// </summary>
        public SingleLinkedNode<T> Nachfolger
        {
            get { return nachfolger; }
            set { nachfolger = value; }
        }

        private T _data;
        private SingleLinkedNode<T> nachfolger;


        /// <summary>
        /// Erstellt einen neuen Knoten.
        /// </summary>
        /// <param name="data">Der Datensatz, der beinhaltet wird.</param>
        public SingleLinkedNode(T data)
        {
            this._data = data;
        }
    }
}
