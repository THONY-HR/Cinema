using System;
using Function;
namespace ClassView
{
    public class LesDiffusions: Fonction
    {
        // Propriétés correspondant aux colonnes de la vue LesDiffusions
        public int IdDiffusion { get; set; } // ID de la diffusion
        public string IdSalle { get; set; } // ID de la salle
        public string NomSalle { get; set; } // Nom de la salle
        public double Paf { get; set; } // Nom de la salle
        public TimeSpan HeureDebut { get; set; } // Heure de début de la diffusion
        public TimeSpan HeureFin { get; set; } // Heure de fin de la diffusion
        public DateTime Daty { get; set; } // Date de la diffusion
        public int IdFilm { get; set; } // ID du film
        public string FilmTitre { get; set; } // Titre du film
        public string Synopsis { get; set; } // Synopsis du film
        public TimeSpan FilmDuree { get; set; } // Durée du film
        public int IdClassification { get; set; } // ID de la classification
        public int AgeClassification { get; set; } // Age de classification
        public int IdCategorie { get; set; } // ID de la catégorie
        public string TypeCategorie { get; set; } // Type de catégorie
        public TimeSpan SalleOuverture { get; set; } // Heure d'ouverture de la salle
        public TimeSpan SalleFermeture { get; set; } // Heure de fermeture de la salle

        // Constructeur avec paramètres
        public LesDiffusions(
            int idDiffusion,
            string idSalle,
            string nomSalle,
            TimeSpan heureDebut,
            TimeSpan heureFin,
            DateTime daty,
            int idFilm,
            string filmTitre,
            string synopsis,
            TimeSpan filmDuree,
            int idClassification,
            int ageClassification,
            int idCategorie,
            string typeCategorie,
            TimeSpan salleOuverture,
            TimeSpan salleFermeture)
        {
            IdDiffusion = idDiffusion;
            IdSalle = idSalle;
            NomSalle = nomSalle;
            HeureDebut = heureDebut;
            HeureFin = heureFin;
            Daty = daty;
            IdFilm = idFilm;
            FilmTitre = filmTitre;
            Synopsis = synopsis;
            FilmDuree = filmDuree;
            IdClassification = idClassification;
            AgeClassification = ageClassification;
            IdCategorie = idCategorie;
            TypeCategorie = typeCategorie;
            SalleOuverture = salleOuverture;
            SalleFermeture = salleFermeture;
        }

        // Constructeur sans paramètres
        public LesDiffusions() { }
    }
}
