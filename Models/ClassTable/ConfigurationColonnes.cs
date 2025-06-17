using System;
using Function;

namespace ClassTable
{
    public class ConfigurationColonnes : Fonction
    {
        public string IdConfig { get; set; } // Correspond à la colonne idConfig (clé primaire)
        public string IdSalle { get; set; } // Correspond à la colonne idSalle (clé étrangère)
        public int Colonne { get; set; } // Correspond à la colonne colonne
        public int Hauteur { get; set; } // Correspond à la colonne hauteur
        public int PlacesParLigne { get; set; } // Correspond à la colonne placesParLigne

        // Constructeur avec paramètres
        public ConfigurationColonnes(string idSalle, int colonne, int hauteur, int placesParLigne)
        {
            IdSalle = idSalle;
            Colonne = colonne;
            Hauteur = hauteur;
            PlacesParLigne = placesParLigne;
        }

        // Constructeur sans paramètres
        public ConfigurationColonnes() { }
    }
}
