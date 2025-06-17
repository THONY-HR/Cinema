using System;
using Function;

namespace ClassView
{
    public class FilmDetails: Fonction
    {
        public int IdFilm { get; set; } // Correspond à la colonne idFilm
        public string Titre { get; set; } // Correspond à la colonne titre
        public string Synopsis { get; set; } // Correspond à la colonne synopsis
        public TimeSpan Duree { get; set; } // Correspond à la colonne duree
        public int AgeClassification { get; set; } // Correspond à la colonne ageClassification (de la table Classification)
        public string TypeCategorie { get; set; } // Correspond à la colonne typeCategorie (de la table Categorie)

        // Constructeur avec paramètres
        public FilmDetails(int idFilm, string titre, string synopsis, TimeSpan duree, int ageClassification, string typeCategorie)
        {
            IdFilm = idFilm;
            Titre = titre;
            Synopsis = synopsis;
            Duree = duree;
            AgeClassification = ageClassification;
            TypeCategorie = typeCategorie;
        }

        // Constructeur sans paramètres
        public FilmDetails() { }
    }
}
