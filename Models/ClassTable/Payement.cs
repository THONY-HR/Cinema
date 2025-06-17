using System;
using Function;

namespace ClassTable
{
    public class Payement : Fonction
    {
        public int IdPayement { get; set; } // Correspond à la colonne idPayement (clé primaire)
        public int IdDiffusion { get; set; } // Correspond à la colonne idDiffusion
        public int IdReservation { get; set; } // Correspond à la colonne idReservation (clé primaire)
        public int IdMethodePayement { get; set; } // Correspond à la colonne idMethodePayement (clé étrangère)
        public double Paf { get; set; } // Correspond à la colonne paf (double pour les montants)

        // Constructeur avec paramètres
        public Payement(int idDiffusion, int idReservation, int idMethodePayement, double paf)
        {
            IdDiffusion = idDiffusion;
            IdReservation = idReservation;
            IdMethodePayement = idMethodePayement;
            Paf = paf;
        }

        // Constructeur sans paramètres
        public Payement() { }
    }
}
