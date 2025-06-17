using System;
using Function;
namespace ClassTable
{
    public class Reservation: Fonction
    {
        public int IdReservation { get; set; }      // ID de la réservation
        public int IdUser { get; set; }             // ID de l'utilisateur
        public int IdDiffusion { get; set; }        // ID de la diffusion
        public string IdDetailSalle { get; set; }   // ID du détail de la salle
        public int IdStatut { get; set; }           // Statut de la réservation (par défaut 2 pour 'réservé')
        public DateTime DatyReservation { get; set; } // Date et heure de la réservation

        public void SetReservation(int idUser,int idDiffusion,string idDetailSalle){
            this.IdUser = idUser;
            this.IdDiffusion = idDiffusion;
            this.IdDetailSalle = idDetailSalle;
        }
    }
}