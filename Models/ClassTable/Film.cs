using System;
using Function;
namespace ClassTable
{
    public class Film: Fonction
    {
        public int IdFilm { get; set; } // Correspond à la colonne idFilm (clé primaire)
        public string Titre { get; set; } // Correspond à la colonne titre
        public int IdClassification { get; set; } // Correspond à la colonne idClassification (clé étrangère)
        public double PrixDiffusion { get; set; } // Correspond à la colonne idClassification (clé étrangère)

        public int IdCategorie { get; set; } // Correspond à la colonne idCategorie (clé étrangère)
        public string Synopsis { get; set; } // Correspond à la colonne synopsis
        public TimeSpan Duree { get; set; } // Correspond à la colonne duree (TIME)

        // Constructeur avec paramètres
        public Film(string titre, int idClassification, int idCategorie, string synopsis, TimeSpan duree,double prixDiffusion)
        {
            Titre = titre;
            IdClassification = idClassification;
            IdCategorie = idCategorie;
            Synopsis = synopsis;
            Duree = duree;
            PrixDiffusion = prixDiffusion;
        }

        // Constructeur sans paramètres
        public Film() { }
    }
}
