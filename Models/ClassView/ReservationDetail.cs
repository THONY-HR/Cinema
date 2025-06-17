using System;
using Function;
namespace ClassView
{
    public class ReservationDetail: Fonction
    {
        public int IdReservation { get; set; }           // ID de la réservation
        public int IdUser { get; set; }                  // ID de l'utilisateur
        public int IdDiffusion { get; set; }             // ID de la diffusion
        public string IdDetailSalle { get; set; }        // ID du détail de la salle
        public int IdStatut { get; set; }                // Statut de la réservation
        public DateTime DatyReservation { get; set; }    // Date de la réservation (DateTime pour inclure l'heure)

        // Informations sur la diffusion
        public string NomSalle { get; set; }             // Nom de la salle
        public TimeSpan DiffusionHeureDebut { get; set; } // Heure de début de la diffusion
        public TimeSpan DiffusionHeureFin { get; set; }   // Heure de fin de la diffusion
        public DateTime DiffusionDate { get; set; }      // Date de la diffusion (DateTime)

        // Informations sur le film
        public string FilmTitre { get; set; }            // Titre du film
        public string FilmSynopsis { get; set; }         // Synopsis du film
        public TimeSpan FilmDuree { get; set; }          // Durée du film (TimeSpan pour la durée)
        public int FilmAge { get; set; }                 // Age de la classification du film
        public string FilmCategorie { get; set; }        // Catégorie du film

        // Informations sur la salle
        public TimeSpan SalleOuverture { get; set; }     // Heure d'ouverture de la salle
        public TimeSpan SalleFermeture { get; set; }     // Heure de fermeture de la salle
    }
}
