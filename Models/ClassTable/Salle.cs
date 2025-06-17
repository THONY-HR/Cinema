using System;
using Function;
namespace ClassTable
{
    public class Salle: Fonction
    {
        public string IdSalle { get; set; } // Correspond à la colonne idSalle (clé primaire)
        public string NomSalle { get; set; } // Correspond à la colonne nomSalle
        public TimeSpan Ouverture { get; set; } // Correspond à la colonne ouverture
        public TimeSpan Fermeture { get; set; } // Correspond à la colonne fermeture

        // Constructeur avec paramètres
        public Salle(string nomSalle, TimeSpan ouverture, TimeSpan fermeture)
        {
            NomSalle = nomSalle;
            Ouverture = ouverture;
            Fermeture = fermeture;
        }

        // Constructeur sans paramètres
        public Salle() { }
    }
}
