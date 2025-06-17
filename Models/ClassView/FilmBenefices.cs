using System;
using Function;
namespace ClassView
{
    public class FilmBenefices: Fonction
    {
        public int IdFilm { get; set; } // Correspond à la colonne idFilm
        public string Titre { get; set; } // Correspond à la colonne titre
        public double PrixDiffusion { get; set; } // Correspond à la colonne prixDiffusion
        public long NombreDeRegard { get; set; } // Correspond à la colonne nombreDeRegard
        public double TotalObtenu { get; set; } // Correspond à la colonne totalObtenu
        public double Benefice { get; set; } // Correspond à la colonne benefice

        // Constructeur avec paramètres
        public FilmBenefices(int idFilm, string titre, double prixDiffusion, int nombreDeRegard, double totalObtenu, double benefice)
        {
            IdFilm = idFilm;
            Titre = titre;
            PrixDiffusion = prixDiffusion;
            NombreDeRegard = nombreDeRegard;
            TotalObtenu = totalObtenu;
            Benefice = benefice;
        }

        // Constructeur sans paramètres
        public FilmBenefices() { }
    }
}
