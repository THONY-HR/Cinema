using System;
using Function;

namespace ClassTable
{
    public class Diffusion : Fonction
    {
        public int IdDiffusion { get; set; } // Correspond à la colonne idDiffusion (clé primaire)
        public string IdSalle { get; set; } // Correspond à la colonne idSalle (clé étrangère)
        public int IdFilm { get; set; } // Correspond à la colonne idFilm (clé étrangère)
        public TimeSpan HeureDebut { get; set; } // Correspond à la colonne heureDebut
        public TimeSpan HeureFin { get; set; } // Correspond à la colonne heureFin
        public double Paf { get; set; } // Correspond à la colonne paf (prix)
        public DateTime Daty { get; set; } // Correspond à la colonne daty (date de la diffusion)

        // Constructeur avec paramètres
        public Diffusion(string idSalle, int idFilm, TimeSpan heureDebut,double paf, DateTime daty)
        {
            IdSalle = idSalle;
            IdFilm = idFilm;
            HeureDebut = heureDebut;
            Paf = paf;
            Daty = daty;
        }

        // Constructeur sans paramètres
        public Diffusion() { }
    }
}
