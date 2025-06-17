using Function;
namespace ClassView
{
    public class PlacesDiffusion: Fonction
    {
        public int IdDiffusion { get; set; }
        public DateTime DateDiffusion { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public string IdSalle { get; set; }
        public string NomSalle { get; set; }
        public string TitreFilm { get; set; }
        public int Colonne { get; set; }
        public int Ligne { get; set; }
        public int Position { get; set; }
        public string IdDetailSalle { get; set; }
        public long IdStatut { get; set; }
        public string StatutSiege { get; set; }
    }
}
