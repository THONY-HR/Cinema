using System;
using Function;

namespace ClassTable
{
    public class ReservationsParJour : Fonction
    {
        public string IdSalle { get; set; } // Correspond à la colonne idSalle
        public DateTime Jour { get; set; } // Correspond à la colonne jour (daty)
        public long NombreReservations { get; set; } // Correspond à la colonne nombreReservations
        public double MontantTotal { get; set; } // Correspond à la colonne montantTotal

        // Constructeur avec paramètres
        public ReservationsParJour(DateTime jour, int nombreReservations, double montantTotal)
        {
            Jour = jour;
            NombreReservations = nombreReservations;
            MontantTotal = montantTotal;
        }

        // Constructeur sans paramètres
        public ReservationsParJour() { }
    }
}
