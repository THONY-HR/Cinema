using System;
using Function;

namespace ClassTable
{
    public class MethodePayement : Fonction
    {
        public int IdMethodePayement { get; set; } // Correspond à la colonne idMethodePayement (clé primaire)
        public string Methode { get; set; } // Correspond à la colonne methode

        // Constructeur avec paramètres
        public MethodePayement(string methode)
        {
            Methode = methode;
        }

        // Constructeur sans paramètres
        public MethodePayement() { }
    }
}
